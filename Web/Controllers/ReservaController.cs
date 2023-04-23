using ApplicationCore.Service;
using Infraestructure.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            IEnumerable<Reserva> lista = null;
            try
            {
                IServiceReserva _ServiceReserva = new ServiceReserva();
                lista = _ServiceReserva.GetReserva(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        public ActionResult /*void*/ CambiarEstado(int id, string correo)
        {
            IServiceReserva _ServiceReserva = new ServiceReserva();
            Reserva oReserva = _ServiceReserva.GetReservaByID(id);
                oReserva.Estado = true;
            
            _ServiceReserva.Save(oReserva);
            _ServiceReserva.Correo(correo, oReserva);
            //LLAMAR METODO CORREO
            //PONER PROMPT DE CORREO ENVIADO
            //return (ActionResult)View("Index");
            return RedirectToAction("Index", "Reserva");
        }

        public ActionResult IndexAdmin()
        {
            IEnumerable<Reserva> lista = null;
            try
            {
                IServiceReserva _ServiceReserva = new ServiceReserva();
                lista = _ServiceReserva.GetReserva(false);
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
            IServiceReserva _ServiceReserva = new ServiceReserva();
            Reserva oReserva = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oReserva = _ServiceReserva.GetReservaByID(Convert.ToInt32(id));
                if (oReserva == null)
                {
                    TempData["Message"] = "No existe el plan solicitado";
                    //Controlador
                    TempData["Redirect"] = "Plan";
                    //Acción
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oReserva);

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

        private SelectList listaAreas(ICollection<AreaComun> areas = null)
        {
            IServiceAreaComun _ServiceAreaComun = new ServiceAreaComun();
            IEnumerable<AreaComun> lista = _ServiceAreaComun.GetAreaComun();
            int[] listaUsuarioSelect = null;
            if (areas != null)
            {
                listaUsuarioSelect = areas.Select(c => c.id).ToArray();
            }

            return new SelectList(lista, "Id", "Descripcion", listaUsuarioSelect);
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            ViewBag.listaUsuarios = listaUsuarios();
            ViewBag.listaAreas = listaAreas();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Reserva reserva)
        {
            //Gestión de archivos
            ModelState.Remove("Total"); 
            reserva.Estado = false;

            //Servicio Libro
            IServiceReserva _ServiceReserva = new ServiceReserva();
            try
            {
                //Insertar la imagen

                if (ModelState.IsValid)
                {
                    Reserva oReserva = _ServiceReserva.Save(reserva);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Utils.ValidateErrors(this);

                    if (reserva.Id > 0)
                    {
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
