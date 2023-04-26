using ApplicationCore.Service;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using Web.ViewModel;
using Log = Web.Utils.Log;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        public ActionResult Ingresos()
        {
            IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
            ViewModelGrafico grafico = new ViewModelGrafico();

            _ServiceAsignacion.GetIngresosMes(out string etiquetas, out string valores);

            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Total $";
            grafico.tituloEtiquetas = "Ingresos anuales por mes de Condominions Caribbean";
            //Tipos de gráficos: bar, bubble, doughnut, pie, line, polarArea
            grafico.tipo = "pie";
            ViewBag.grafico = grafico;
            return View();
        }

        public ActionResult AsignacionDeuda()
        {

            try
            {
                IEnumerable<Asignacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    // Obtener la lista de residencias disponibles
                    var residencia = ctx.Residencia.ToList();
                    // Crear la lista de opciones para el DropDownList de residencias
                    List<SelectListItem> residenciaOptiones = residencia.Select(r => new SelectListItem
                    {
                        Text = r.NoCondominio.ToString(),
                        Value = r.NoCondominio.ToString(),
                    }).ToList();



                    // Pasar la lista de residencias a la vista
                    ViewBag.residencia = residenciaOptiones;

                    List<SelectListItem> months = new List<SelectListItem>();

                    for (int i = 1; i <= 12; i++)
                    {
                        months.Add(new SelectListItem { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), Value = i.ToString() });
                    }

                    ViewBag.mes = months;
                    IRepositoryReporte _ServiceReporte = new RepositoryReporte();
                    lista = _ServiceReporte.GetDeudas();
                    return View(lista);
                }
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public PartialViewResult DeudaxResidencia(int? id)
        {
            //Contenido a actualizar
            IEnumerable<Asignacion> lista = null;
            IServiceReporte _ServicioLibro = new ServiceReporte();
            if (id != null)
            {
                lista = _ServicioLibro.GetAsignacionByIdResidencia((int)id);
                //Convert.ToInt32(id)
            }
            //Nombre de vista parcial, datos para la vista
            return PartialView("AsignacionDeuda", lista);
        }

        private SelectList listaResidencia(int idResidencia = 0)
        {

            IServiceResidencia _ServiceAutor = new ServiceResidencia();
            IEnumerable<Residencia> listaResidencia = _ServiceAutor.GetResidencia();
            return new SelectList(listaResidencia, "Id", "NoCondominio", idResidencia);
        }
        /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult CreatePdfLibroCatalogo(int? residencia, int? mes)
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Asignacion> lista = null;
            try
            {
                // Extraer informacion
                IRepositoryReporte _ServiceReporte = new RepositoryReporte();
                lista = _ServiceReporte.GetDeudas().Where(p => (!residencia.HasValue || p.Residencia.NoCondominio == residencia.Value)
           && (!mes.HasValue || p.FechaPago.Month == mes.Value));


                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Inicializar writer
                PdfWriter writer = new PdfWriter(ms);

                //Inicializar document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                //Título
                Paragraph header = new Paragraph("Deudas")
                                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                    .SetFontSize(14)
                                    .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);
                // Crear tabla con 5 columnas 
                Table table = new Table(4, true);
                //Encabezados de la tabla
                table.AddHeaderCell("Residencia");
                table.AddHeaderCell("Plan");
                table.AddHeaderCell("Fecha de pago");
                table.AddHeaderCell("Deuda");

                foreach (var item in lista)
                {
                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Residencia.NoCondominio.ToString()));
                    table.AddCell(new Paragraph(item.Plan.Descrpcion));
                    table.AddCell(new Paragraph(String.Format("{0:dd-MM-yyyy}", item.FechaPago)));
                    table.AddCell((item.Deuda) ? "No pagado" : "Pagado");

                }
                doc.Add(table);

                // Colocar número de páginas
                int numberofPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberofPages; i++)
                {
                    //Texto alineado a un punto específico
                    doc.ShowTextAligned(
                        new Paragraph(
                            String.Format("página {0} de {1}", i, numberofPages)
                            ),
                        559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0
                        );
                }
            

                //Terminar document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");

            
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        // GET: Reporte/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporte/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporte/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reporte/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporte/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reporte/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
