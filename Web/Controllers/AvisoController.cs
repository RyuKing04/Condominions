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
    public class AvisoController : Controller
    {
        // GET: Aviso
        public ActionResult Index()
        {
            IEnumerable<Avisos> lista = null;
            try
            {
                IServiceAvisos _ServiceAvisos = new ServiceAvisos();
                lista = _ServiceAvisos.GetAvisos();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: Aviso/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Aviso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

    }
}
