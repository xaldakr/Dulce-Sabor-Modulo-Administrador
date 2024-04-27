using Dulce_Sabor_Modulo_Administrador.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dulce_Sabor_Modulo_Administrador.Controllers
{
    public class AdminController : Controller
    {

        private readonly AdminContext _AdminContexto;
        public AdminController(AdminContext AdminContext)
        {
            _AdminContexto = AdminContext;
        }
        public IActionResult Index()
        {
            double? suma;
            DateTime fechaHoy = ObtenerFechaHoy();
            //Obtener primero los datos del viewbag
            int mesasdisp = (from e in _AdminContexto.Mesa
                             where e.estado_mesa == false
                             select e.id).ToList().Count();

            int mesasocup = (from e in _AdminContexto.Mesa
                             where e.estado_mesa == true
                             select e.id).ToList().Count();

            int pedpros = (from e in _AdminContexto.Cuentas
                           where e.estado.Equals("ABIERTO")
                           select e.id_cuenta).ToList().Count();

            int pedfin = (from e in _AdminContexto.Cuentas
                          where e.estado.Equals("CERRADO")
                          select e.id_cuenta).ToList().Count();

            suma = (double?)(from e in _AdminContexto.DetalleCuentas
                             join m in _AdminContexto.Cuentas on 
                             e.id_cuenta equals m.id_cuenta
                             where m.fecha_apertura >= fechaHoy && e.estado.Equals("CANCELADO")
                             select e.precio * (1 - e.descuento)).ToList().Sum();

            double ingrecuen = suma == null ? 0 : (double)suma;

            suma = (from e in _AdminContexto.DetalleVentas
                            where e.fecha_creacion >= fechaHoy
                            select e.precio).ToList().Sum();

            double ingrevent = suma == null ? 0 : (double)suma;

            dynamic DatosDash = new
            {
                MesasD = mesasdisp,
                MesasO = mesasocup,
                PedA = pedpros,
                PedC = pedfin,
                IngC = ingrecuen,
                IngV = ingrevent,
                Tot = ingrecuen + ingrevent
            };

            ViewBag.Dash = DatosDash;
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
        private double SumarVenta(int id)
        {
            double? suma = (from e in _AdminContexto.DetalleVentas
                           where e.id_venta == id 
                           select e.precio).ToList().Sum();
            double darsuma;
            darsuma = suma == null ? 0: (double)suma;
            return darsuma;
        }
        private double SumarCuenta(int id)
        {
            double? suma = (double?) (from e in _AdminContexto.DetalleCuentas
                            where e.id_cuenta == id && e.estado.Equals("CANCELADO")
                            select e.precio * (1 - e.descuento ) ).ToList().Sum();
            double darsuma;
            darsuma = suma == null ? 0 : (double)suma;
            return darsuma;
        }
        private static DateTime ObtenerFechaHoy()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaMinima = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 0, 0, 0);

            return fechaMinima;
        }
    }
}
