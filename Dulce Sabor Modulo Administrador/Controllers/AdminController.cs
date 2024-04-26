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
            return RedirectToAction("tablaMes");
        }
        //Index a tablaMes
        public IActionResult enviarMes()
        {
            return RedirectToAction("tablaMes");
        }
        //Tabla de Ventas en Linea
        public IActionResult tablaLinea()
        {
            return View();
        }
        //Interno a tablaLinea
        public IActionResult formularioLinea()
        {
            return RedirectToAction("tablaLinea");
        }
        //Index a tablaLinea
        public IActionResult enviarLinea()
        {
            return RedirectToAction("tablaLinea");
        }
        //Tabla segun Mesero
        public IActionResult tablaMesero()
        {
            return View();
        }
        //Interno a tablaMesero
        public IActionResult formularioMesero()
        {
            return RedirectToAction("tablaMesero");
        }
        //Index a tablaMesero
        public IActionResult enviarMesero()
        {
            return RedirectToAction("tablaMesero");
        }
        //regreso general
        public IActionResult regresar()
        {
            return RedirectToAction("Index");
        }
    }
}
