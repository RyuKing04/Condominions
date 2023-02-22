using ApplicationCore.Service;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class AsignacionController : Controller
    {
        // GET: Asignacion
        public ActionResult Index()
        {
            IEnumerable<Asignacion> lista = null;
            try
            {
                IServiceAsignacion _ServiceAsignacion = new ServiceAsignacion();
                lista = _ServiceAsignacion.GetAsignacion();
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
        public ActionResult Details(int id, bool active)
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

        // GET: Asignacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asignacion/Create
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
    }
}
