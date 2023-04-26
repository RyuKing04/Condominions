using ApplicationCore.Service;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class AsignacionController : Controller
    {
        // GET: Asignacion
        //[CustomAuthorize(Roles.Administrador.ToString())]
        public ActionResult Index(DateTime fecha) //el admin ve los usuarios con asignaciones del mes actual
        {
            IEnumerable<Asignacion> lista = null;
            try
            {
                IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
                lista = _ServiceAsignacion.GetAsignacion(fecha);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        public ActionResult IndexUsuario(int id) //el usuario ve sus propias asignaciones 
        {
            IEnumerable<Asignacion> lista = null;
            try
            {
                IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
                lista = _ServiceAsignacion.GetAsignacionByIdUsuario(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: Asignacion/Details/5
        public ActionResult Details(int? id, bool active) // carga dependiendo si tiene deudas o no el usuario vista solo para el admin
        {

            IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
            IEnumerable<Asignacion> oAsignacion = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oAsignacion = _ServiceAsignacion.GetAsignacionByIdResidencia(Convert.ToInt32(id),active);
                if (oAsignacion == null)
                {
                    TempData["Message"] = "No existe la residencia solicitado";
                    //Controlador
                    TempData["Redirect"] = "Residencia";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oAsignacion);

            }
            catch (Exception ex)
            {

                TempData["Message"] = "Error al procesar los datos" + ex.Message;
                //Controlador
                TempData["Redirect"] = "Residencia";
                //Acción
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaResidencias(ICollection<Residencia> residencias = null)
        {
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            IEnumerable<Residencia> lista = _ServiceResidencia.GetResidenciaByFechaAsignacion(DateTime.Now);
            
            int[] listaResidenciaSelect = null;
            if (residencias != null)
            {
                listaResidenciaSelect = residencias.Select(c => c.id).ToArray();
            }
            
            return new SelectList(lista, "id", "NoCondominio", listaResidenciaSelect);
        }

        private SelectList listaPlanes(ICollection<Plan> planes = null)
        {
            IServicePlan _ServicePlan = new ServicePlan();
            IEnumerable<Plan> lista = _ServicePlan.GetPlan();

            int[] listaPlanesSelect = null;
            if (planes != null)
            {
                listaPlanesSelect = planes.Select(c => c.Id).ToArray();
            }

            return new SelectList(lista, "Id", "Descrpcion", listaPlanesSelect);
        }

        public ActionResult /*void*/ CambiarDeuda(int id)
        {
            IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
            Asignacion oAsignacion = _ServiceAsignacion.GetAsignacionbyId(id);
            oAsignacion.Deuda = false;

            _ServiceAsignacion.SaveAsignacion(oAsignacion);
            return RedirectToAction("Index", "Reserva");
        }

        //[CustomAuthorize((int)Roles.Administrador)]
        // GET: Asignacion/Create
        public ActionResult Create()
        {
            ViewBag.listaResidencias = listaResidencias();
            ViewBag.listaPlanes = listaPlanes();
            return View();
        }

        // GET: Asignacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Asignacion/Edit/5
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

        // GET: Asignacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asignacion/Delete/5
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

        [HttpPost]
        public ActionResult Save(Asignacion pAsignacion)
        {
            ModelState.Remove("Id");
            ModelState.Remove("EstadoCuenta");

            pAsignacion.Deuda = true;

            IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
            try
            {
                if (ModelState.IsValid)
                {
                    Asignacion oAsignacion = _ServiceAsignacion.SaveAsignacion(pAsignacion);
                }
                else
                {

                    //Valida Errores si Javascript está deshabilitado
                    Utils.Utils.ValidateErrors(this);

                    if (pAsignacion.Id > 0)
                    {
                        return (ActionResult)View("Index");
                    }
                    else
                    {
                        return View("Create");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Aviso";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
