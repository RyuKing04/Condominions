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
    public class AreaComunController : Controller
    {
        // GET: AreaComun
        public ActionResult Index()
        {
            IEnumerable<AreaComun> lista = null;
            try
            {
                IServiceAreaComun _ServiceAreaComun = new ServiceAreaComun();
                lista = _ServiceAreaComun.GetAreaComun();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: AreaComun/Details/5
        public ActionResult Details(int? id)
        {
            IServiceAreaComun _ServiceAreaComun = new ServiceAreaComun();
            AreaComun oAreaComun = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oAreaComun = _ServiceAreaComun.GetAreaComunByID(Convert.ToInt32(id));
                if (oAreaComun == null)
                {
                    TempData["Message"] = "No existe la residencia solicitado";
                    //Controlador
                    TempData["Redirect"] = "Residencia";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oAreaComun);
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

        // POST: AreaComun/Create
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

        // GET: AreaComun/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AreaComun/Edit/5
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

        // GET: AreaComun/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AreaComun/Delete/5
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
