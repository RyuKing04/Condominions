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
    public class LoginController : Controller
    {
        public ActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {
                //ModelState.Remove("Id");
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellido");
                ModelState.Remove("Rol");
                ModelState.Remove("FechaNacimiento");
                ModelState.Remove("Estado");
                ModelState.Remove("Contrasenna");
                //Verificar las credenciales

                if (ModelState.IsValid)
                {
                    Usuario pUsuario = _ServiceUsuario.GetUsuario(usuario.Email, usuario.Contrasenna1);
                    if (pUsuario != null)
                    {

                        Session["User"] = _ServiceUsuario.GetUsuarioByID(pUsuario.Id); 
                        
                        Log.Info($"Inicio sesion: {pUsuario.Email}");

                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Login",
                            "Usuario autenticado", Util.SweetAlertMessageType.success
                            );
                        return RedirectToAction("IndexUsuario", "Aviso");
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio: {usuario.Email}");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                            "Usuario no válido", Util.SweetAlertMessageType.error
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View("Index");
        }

        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                Usuario usuario = Session["User"] as Usuario;
                Log.Warn($"No autorizado {usuario.Email}");
            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                //Eliminar variable de sesion
                Session["User"] = null;
                Session.Remove("User");

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
