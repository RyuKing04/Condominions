﻿using Antlr.Runtime.Misc;
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
    public class AvisoController : Controller
    {
        private bool oActive;
        // GET: Aviso
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

        // GET: Aviso/Create
        public ActionResult Create(bool active)
        {
            //Avisos oAviso = new Avisos();
            //if (active)
            //{
            //    //informacion
            //    oAviso.idUsuario = 1;
            //    oAviso.TipoAviso = "Información";
            //    oAviso.Estado = "NA";
            //}
            //else
            //{
            //    //incidencia
            //    oAviso.Fecha = DateTime.Now;
            //    oAviso.TipoAviso = "Incidencia";
            //    oAviso.Estado = "En proceso";
            //}
            oActive = active;
            ViewBag.listaUsuarios = listaUsuarios();
            ViewBag.active = active;
            return View();
        }

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

        [HttpPost]
        public ActionResult Save(Avisos aviso, HttpPostedFileBase ImageFile)
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

                if (ModelState.IsValid)
                {
                    Avisos oAviso = _ServiceAviso.SaveAvisos(aviso, oActive);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Utils.ValidateErrors(this);

                    if (aviso.id > 0)
                    {
                        return (ActionResult)View("Edit", aviso);
                    }
                    else
                    {
                        return View("Create", aviso);
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
