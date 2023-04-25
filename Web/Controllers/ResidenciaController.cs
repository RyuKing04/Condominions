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
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Residencia> lista = null;
            try
            {
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                lista = _ServiceResidencia.GetResidencia();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

       // [CustomAuthorize((int)Roles.Administrador)]
        // GET: Residencia/Details/5
        public ActionResult Details(int? id)
        {
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            Residencia oResidencia = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oResidencia = _ServiceResidencia.GetResidenciaByID(Convert.ToInt32(id));
                if (oResidencia == null)
                {
                    TempData["Message"] = "No existe la residencia solicitado";
                    //Controlador
                    TempData["Redirect"] = "Residencia";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oResidencia);

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

       // [CustomAuthorize((int)Roles.Administrador)]
        // GET: Residencia/Create
        public ActionResult Create()
        {
            ViewBag.listaUsuarios = listaUsuarios();
            return View();
        }

        // POST: Residencia/Create
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

        // GET: Residencia/Edit/5
        public ActionResult Edit(int? id)
        {
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            Residencia residencia= null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                residencia = _ServiceResidencia.GetResidenciaByID(Convert.ToInt32(id));
                if (residencia == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados

                return View(residencia);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        private SelectList listaUsuarios(ICollection<Usuario> usuarios = null)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> lista = _ServiceUsuario.GetUsuario();
            int[] listaUsuarioSelect = null;
            if (usuarios != null)
            {
                listaUsuarioSelect = usuarios.Select(c => c.Id).ToArray();
            }

            return new SelectList(lista, "Id", "Nombre", listaUsuarioSelect);
        }
        // POST: Residencia/Edit/5

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

        // GET: Residencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Residencia/Delete/5
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
        public ActionResult Save(Residencia residencia)
        {
            //Gestión de archivos
            MemoryStream target = new MemoryStream();
            //Servicio Libro
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            try
            {

                if (ModelState.IsValid)
                {
                    Residencia oResidencia = _ServiceResidencia.Save(residencia);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //Utils.Util.ValidateErrors(this);

                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (residencia.id > 0)
                    {
                        return (ActionResult)View("Edit", residencia);
                    }
                    else
                    {
                        return View("Create", residencia);
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
