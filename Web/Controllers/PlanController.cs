using ApplicationCore.Service;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index() //trae todos los datos
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

        // GET: Plan/Details/5
        public ActionResult Details(int id) //trae los datos de un plan en específico
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
        private MultiSelectList listaRubro(ICollection<Rubro> rubros = null)
        {
            IServiceRubro _ServiceRubro = new ServiceRubro();
            IEnumerable<Rubro> lista = _ServiceRubro.GetRubro();
            //Seleccionar categorias
            int[] listaRubroSelect = null;
            if (rubros != null)
            {
                listaRubroSelect = rubros.Select(c => c.Id).ToArray();
            }

            return new MultiSelectList(lista, "Id", "Descrpicion", listaRubroSelect);
        }

        // GET: Plan/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.listaRubros = listaRubro();
            return View();
        }

        // POST: Plan/Create
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

        [HttpPost]
        public ActionResult Save(Plan plan, string[] selectedRubros)
        {
            IServicePlan _ServicePlan = new ServicePlan();
            try
            {
                if (ModelState.IsValid)
                {
                    Plan oPlan = _ServicePlan.SavePlan(plan, selectedRubros);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //System.Web.Util.ValidateErrors(this);
                    ViewBag.listaRubros = listaRubro(plan.Rubro);
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (plan.Id > 0)
                    {
                        return (ActionResult)View("Edit", plan);
                    }
                    else
                    {
                        return View("Create", plan);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PLan";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Plan/Edit/5
        public ActionResult Edit(int id)
        {
            IServicePlan _ServicePlan = new ServicePlan();
            Plan plan = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                plan = _ServicePlan.GetPlanByID(Convert.ToInt32(id));
                if (plan == null)
                {
                    TempData["Message"] = "No existe el plan solicitado";
                    TempData["Redirect"] = "Plan";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
                ViewBag.listaRubros = listaRubro(plan.Rubro);
                return View(plan);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Plan";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Plan/Edit/5
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

        // GET: Plan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Plan/Delete/5
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
