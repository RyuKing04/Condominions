using ApplicationCore.Service;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class RubroController : Controller
    {
        // GET: Rubro
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Rubro> lista = null;
            try
            {
                IServiceRubro _ServiceRubro = new ServiceRubro();
                lista = _ServiceRubro.GetRubro();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Rubro/Details/5
        public ActionResult Details(int? id)
        {
            IServiceRubro _ServiceRubro = new ServiceRubro();
            Rubro oRubro = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oRubro = _ServiceRubro.GetRubroByID(Convert.ToInt32(id));
                if (oRubro == null)
                {
                    TempData["Message"] = "No existe la residencia solicitado";
                    //Controlador
                    TempData["Redirect"] = "Residencia";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oRubro);

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

        // GET: Rubro/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Rubro/Create
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

        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Rubro/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceRubro _ServiceRubro = new ServiceRubro();
            Rubro rubro = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                rubro = _ServiceRubro.GetRubroByID(Convert.ToInt32(id));
                if (rubro == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
              
                return View(rubro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Rubro/Edit/5
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

        // GET: Rubro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rubro/Delete/5
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
        public ActionResult Save(Rubro rubro)
        {
            //Gestión de archivos
            MemoryStream target = new MemoryStream();
            //Servicio Libro
            IServiceRubro _ServiceRubro = new ServiceRubro();
            try
            {
                
                if (ModelState.IsValid)
                {
                    Rubro oRubro = _ServiceRubro.SaveRubro(rubro);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Utils.Util.ValidateErrors(this);
    
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (rubro.Id > 0)
                    {
                        return (ActionResult)View("Edit", rubro);
                    }
                    else
                    {
                        return View("Create", rubro);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Rubro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
