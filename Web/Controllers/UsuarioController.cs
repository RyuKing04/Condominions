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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuario();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }
        public ActionResult /*void*/ CambiarEstado(int id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            _ServiceUsuario.CambiarEstado(id);
            return RedirectToAction("Index", "Usuario");
        }
        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oUsuario = _ServiceUsuario.GetUsuarioByID(Convert.ToInt32(id));
                if (oUsuario == null)
                {
                    TempData["Message"] = "No existe la residencia solicitado";
                    //Controlador
                    TempData["Redirect"] = "Residencia";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oUsuario);
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
        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        //[CustomAuthorize((int)Roles.Administrador)]
        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Save(Usuario pUsuario)
        {
            ModelState.Remove("Estado");
            ModelState.Remove("Rol");
            ModelState.Remove("Contrasenna");

            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario oUsuario = _ServiceUsuario.Save(pUsuario);
                }
                else
                {
                    if (pUsuario.Id > 0)
                    {
                    }
                    else
                    {
                        return View("Create", pUsuario);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Index";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

    }
}
