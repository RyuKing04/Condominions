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
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult Index()
        {
            IEnumerable<Plan> lista = null;
            try
            {
                IServicePlan _ServicePlan = new ServicePlan();
                lista = _ServicePlan.GetPlan();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: Reserva/Details/5
        public ActionResult Details(int? id)
        {
            IServicePlan _ServicePlan = new ServicePlan();
            Plan oPlan = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oPlan = _ServicePlan.GetPlanByID(Convert.ToInt32(id));
                if (oPlan == null)
                {
                    TempData["Message"] = "No existe el plan solicitado";
                    //Controlador
                    TempData["Redirect"] = "Plan";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oPlan);

            }
            catch (Exception ex)
            {

                TempData["Message"] = "Error al procesar los datos" + ex.Message;
                //Controlador
                TempData["Redirect"] = "Plan";
                //Acción
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reserva/Create
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

        // GET: Reserva/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reserva/Edit/5
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

        // GET: Reserva/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reserva/Delete/5
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
