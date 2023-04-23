using Antlr.Runtime.Misc;
using ApplicationCore.Service;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class AvisoController : Controller
    {
        // GET: Aviso
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index(bool active)
        {
            IEnumerable<Avisos> lista = null;
            try
            {
                ViewBag.active = active;
                IServiceAvisos _ServiceAvisos = new ServiceAvisos();
                lista = _ServiceAvisos.GetAvisos(active);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        public ActionResult IndexUsuario()
        {
            IEnumerable<Avisos> lista = null;
            try
            {
                IServiceAvisos _ServiceAvisos = new ServiceAvisos();
                lista = _ServiceAvisos.GetAvisosUsuario();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
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

        private SelectList listaTiposInfo()
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<string> lista = new string[] { "Noticias", "Avisos", "Archivos Documentales" };

            return new SelectList(lista);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Aviso/Create
        public ActionResult Create(bool active)
        {
            ViewBag.listaUsuarios = listaUsuarios();
            ViewBag.listaTipoInfo = listaTiposInfo();
            ViewBag.active = active;
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Aviso/Edit/5
        public ActionResult Edit(int? id, bool active)
        {
            ServiceAvisos _ServiceAvisos = new ServiceAvisos();
            Avisos aviso = null;
            ViewBag.active = active;
            
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                aviso = _ServiceAvisos.GetAvisosByID(Convert.ToInt32(id));
                if (aviso == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Aviso";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados

                return View(aviso);
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

        public void CambiarEstado(int id)
        {
            IServiceAvisos _ServiceAviso = new ServiceAvisos();
            Avisos aviso = _ServiceAviso.GetAvisosByID(id);
            if(aviso.EstadoTipoInfo == "En proceso")
            {
                aviso.EstadoTipoInfo = "Finalizado";
            }

            _ServiceAviso.SaveAvisos(aviso, false);
        }

        [HttpPost]
        public ActionResult Save(Avisos aviso, HttpPostedFileBase ImageFile, bool activo)
        {
            //Gestión de archivos
            MemoryStream target = new MemoryStream();
            //Servicio Libro
            IServiceAvisos _ServiceAviso = new ServiceAvisos();
            try
            {
                //Insertar la imagen

                if (ImageFile != null)
                {
                    ImageFile.InputStream.CopyTo(target);
                    aviso.Documento = target.ToArray();
                    ModelState.Remove("Documento");
                }

                //if (ModelState.IsValid)
                //{
                    Avisos oAviso = _ServiceAviso.SaveAvisos(aviso, activo);
               // }
                //else
                //{
                    //Valida Errores si Javascript está deshabilitado
                    //Utils.Utils.ValidateErrors(this);

                    //if (aviso.id > 0)
                    //{
                    //    return (ActionResult)View("Edit", aviso, active);
                    //}
                    //else
                    //{
                    //    return View("Create", aviso, new { });
                    //}
                //}

                return RedirectToAction("Index", routeValues: new {active = activo });
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
