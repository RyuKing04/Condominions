using ApplicationCore.Service;
using Infraestructure.Models;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using Log = Web.Utils.Log;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        public ActionResult Deuda()
        {
            return View();
            //IEnumerable<Asignacion> lista = null;
            //try
            //{
            //    IServiceReporte _ServiceReporte = new ServiceReporte();
            //    lista = _ServiceReporte.GetReporteDeuda(fecha);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, MethodBase.GetCurrentMethod());
            //    TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            //    return RedirectToAction("Default", "Error");
            //}
            //return View(lista);
        }

        public ActionResult Ingresos(DateTime fecha)
        {
            IEnumerable<Asignacion> lista = null;
            try
            {
                IServiceAsignacion _ServiceReporte = new ServiceAsignacion();
                lista = _ServiceReporte.GetAsignacion(fecha);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AsignacionDeuda(DateTime fecha)
        {
            IEnumerable<Asignacion> lista = null;
            try
            {

                IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
                lista = _ServiceAsignacion.GetAsignacion(fecha);
                return View(lista);
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

        /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult CreatePdfLibroCatalogo(DateTime fecha)
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Asignacion> lista = null;
            try
            {
                // Extraer informacion
                IServiceAsignacion _ServiceLibro = new ServiceAsignacion();
                lista = _ServiceLibro.GetAsignacion(fecha);

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
                    table.AddCell(new Paragraph(String.Format("{0}", item.Deuda)));
                  
                    // Convierte la imagen que viene en Bytes en imagen para PDF

                    //Tamaño de la imagen
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
