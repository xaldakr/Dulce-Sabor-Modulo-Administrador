using Microsoft.AspNetCore.Mvc;

namespace Dulce_Sabor_Modulo_Administrador.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //Tabla segun fecha
        public IActionResult tablaFechas()
        {
            return View();
        }
        //Interno a tablaFechas
        public IActionResult formularioFechas()
        {
            return RedirectToAction("tablaFechas");
        }
        //Index a tablaFechas
        public IActionResult enviarFecha()
        {
            return RedirectToAction("tablaFechas");
        }
        //Tabla segun Mes
        public IActionResult tablaMes()
        {
            return View();
        }
        //Interno a tablaMes
        public IActionResult formularioMes()
        {
            return RedirectToAction("tablaFechas");
        }
        //Index a tablaMes
        public IActionResult enviarMes()
        {
            return RedirectToAction("tablaFechas");
        }
        //regreso general
        public IActionResult regresar()
        {
            return RedirectToAction("Index");
        }
    }
}
