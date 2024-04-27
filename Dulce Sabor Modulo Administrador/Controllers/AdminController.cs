using Dulce_Sabor_Modulo_Administrador.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Dulce_Sabor_Modulo_Administrador.Controllers
{
    public class AdminController : Controller
    {

        private readonly AdminContext _AdminContexto;
        public static DateTime fechaini;
        public static DateTime fechafin;
        public static DateTime fechaini2;
        public static DateTime fechafin2;
        public static DateTime fechaMesini;
        public static DateTime fechaMesfin;
        public static string noMesero;
        public AdminController(AdminContext AdminContext)
        {
            _AdminContexto = AdminContext;
            if(fechaini == DateTime.MinValue) { 
                fechaini = DateTime.Now;
                fechaini = new DateTime(fechaini.Year, fechaini.Month, fechaini.Day, 0, 0, 0);
            }
            if (fechaini2 == DateTime.MinValue)
            {
                fechaini2 = DateTime.Now;
                fechaini2 = new DateTime(fechaini2.Year, fechaini2.Month, fechaini2.Day, 0, 0, 0);
            }
            if (fechafin == DateTime.MinValue)
            {
                fechafin = DateTime.Now;
                fechafin = new DateTime(fechafin.Year, fechafin.Month, fechafin.Day, 23,59,59);
            }
            if (fechafin2 == DateTime.MinValue)
            {
                fechafin2 = DateTime.Now;
                fechafin2 = new DateTime(fechafin2.Year, fechafin2.Month, fechafin2.Day, 23, 59, 59);
            }
            if (fechaMesini == DateTime.MinValue)
            {
                fechaMesini = DateTime.Now;
                fechaMesini = new DateTime(fechaMesini.Year, fechaMesini.Month, 1, 0,0,0);
            }
            if(fechaMesfin == DateTime.MinValue)
            {
                fechaMesfin = DateTime.Now;
                fechaMesfin = new DateTime(fechaMesfin.Year, fechaMesfin.Month, DateTime.DaysInMonth(fechaMesfin.Year, fechaMesfin.Month), 23, 59, 59);
            }
            if(noMesero == null) {
                noMesero = "";
            }
        }
        public IActionResult Index()
        {
            //Inicializar variables locales
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
                           where e.estado.Equals("ABIERTO") && e.fecha_apertura >= fechaHoy
                           select e.id_cuenta).ToList().Count();

            int pedfin = (from e in _AdminContexto.Cuentas
                          where e.estado.Equals("CERRADO") && e.fecha_apertura >= fechaHoy
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

            var cuentas = _AdminContexto.Cuentas.Where(c => c.fecha_apertura >= fechaHoy).ToList();

            var datostabla = (from c in cuentas
                              join m in _AdminContexto.Mesa
                              on c.id_mesa equals m.id
                              select new
                              {
                                  Nmesa = m.numero_mesa,
                                  NCuenta = c.id_cuenta,
                                  FechaC = c.fecha_apertura.ToString("HH:mm:ss"),
                                  Estado = c.estado,
                                  Ingresos = SumarCuenta(c.id_cuenta, _AdminContexto)
                              }).ToList();
            ViewData["datosTabla1"] = datostabla;
            return View();
        }
        //Tabla segun fecha
        public IActionResult tablaFechas()
        {
            var cuentas = (from c in _AdminContexto.Cuentas where c.fecha_apertura >= fechaini && c.fecha_apertura <= fechafin select c).ToList();

            var datostabla = (from c in cuentas
                              join m in _AdminContexto.Empleado
                              on c.id_empleado equals m.id
                              select new
                              {
                                  NCuenta = c.id_cuenta,
                                  FechaC = c.fecha_apertura.ToString("HH:mm:ss"),
                                  Mesero = m.nombre,
                                  Ingresos = SumarCuenta(c.id_cuenta, _AdminContexto)
                              }).ToList();
            ViewData["datosTabla2"] = datostabla;
            ViewBag.Fechatf = fechaini;
            return View();
        }
        //Interno a tablaFechas
        public IActionResult formularioFechas(DateTime fecha)
        {
            if (fecha == DateTime.MinValue)
            {
                fecha = DateTime.Now;
            }
            fechaini = fecha;
            fechaini = new DateTime(fechaini.Year, fechaini.Month, fechaini.Day, 0, 0, 0);
            fechafin = fecha;
            fechafin = new DateTime(fechafin.Year, fechafin.Month, fechafin.Day, 23, 59, 59);
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
            var mesesYAniosConDatos = _AdminContexto.Cuentas
            .Select(c => new { Mes = new DateTime(c.fecha_apertura.Year, c.fecha_apertura.Month, 1),
                                DatoMes = (new DateTime(c.fecha_apertura.Year, c.fecha_apertura.Month, 1).ToString("MMMM yyyy").ToUpper())
            })
            .Distinct()
            .ToList();

            ViewData["datosCol1"] = new SelectList(mesesYAniosConDatos, "Mes", "DatoMes");

            var cuentas = (from c in _AdminContexto.Cuentas where c.fecha_apertura >= fechaMesini && c.fecha_apertura <= fechaMesfin select c).ToList();

            var datostabla = (from c in cuentas
                              join m in _AdminContexto.Empleado
                              on c.id_empleado equals m.id
                              select new
                              {
                                  NCuenta = c.id_cuenta,
                                  FechaC = c.fecha_apertura.ToString("dd MMMM yyyy  HH:mm:ss").ToUpper(),
                                  Mesero = m.nombre,
                                  Ingresos = SumarCuenta(c.id_cuenta, _AdminContexto)
                              }).ToList();
            ViewData["datosTabla3"] = datostabla;
            ViewBag.FechaM = fechaMesini;
            return View();
        }
        //Interno a tablaMes
        public IActionResult formularioMes(DateTime Mes)
        {
            if (Mes == DateTime.MinValue)
            {
                Mes = DateTime.Now;
                fechaMesini = new DateTime(Mes.Year, Mes.Month, 1, 0, 0, 0);
                fechaMesfin = new DateTime(Mes.Year, Mes.Month, DateTime.DaysInMonth(Mes.Year, Mes.Month), 23, 59, 59);
            }
            else { 
                fechaMesini = new DateTime(Mes.Year, Mes.Day, 1, 0, 0, 0);
                fechaMesfin = new DateTime(Mes.Year, Mes.Day, DateTime.DaysInMonth(Mes.Year, Mes.Day), 23, 59, 59);
            }
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
            var ventas = (from c in _AdminContexto.Ventas where c.fecha >= fechaini2 && c.fecha <= fechafin2 select c).ToList();

            var datostabla = (from c in ventas
                              select new
                              {
                                  NCuenta = c.id,
                                  FechaC = c.fecha.ToString("dd MMMM yyyy  HH:mm:ss").ToUpper(),
                                  Ingresos = SumarVenta(c.id, _AdminContexto)
                              }).ToList();
            ViewData["datosTabla4"] = datostabla;
            ViewBag.Fechati = fechaini2;
            ViewBag.Fechatfi = fechafin2;
            return View();
        }
        //Interno a tablaLinea
        public IActionResult formularioLinea(DateTime fechai, DateTime fechaf)
        {
            if (fechai == DateTime.MinValue)
            {
                fechai = DateTime.Now;
            }
            if (fechaf == DateTime.MinValue)
            {
                fechaf = DateTime.Now;
            }
            fechaini2 = fechai;
            fechaini2 = new DateTime(fechai.Year, fechai.Month, fechai.Day, 0, 0, 0);
            fechafin2 = fechaf;
            fechafin2 = new DateTime(fechaf.Year, fechaf.Month, fechaf.Day, 23, 59, 59);
            if (fechai > fechaf)
            {
                fechaini2 = DateTime.Now;
                fechaini2 = new DateTime(fechaini2.Year, fechaini2.Month, fechaini2.Day, 0, 0, 0);
                fechafin2 = DateTime.Now;
                fechafin2 = new DateTime(fechafin2.Year, fechafin2.Month, fechafin2.Day, 23, 59, 59);
            }
            
            
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
            var meseros = (from m in _AdminContexto.Empleado
                           join c in _AdminContexto.Cuentas on m.id equals c.id_empleado
                           where m.nombre.Contains(noMesero) select new
                           {
                               idcuen = c.id_cuenta,
                               nombres = m.nombre,
                               fechaap = c.fecha_apertura,
                           }).ToList();

            var datostabla = (from c in meseros
                              select new
                              {
                                  NCuenta = c.idcuen,
                                  FechaC = c.fechaap.ToString("HH:mm:ss"),
                                  Mesero = c.nombres,
                                  Ingresos = SumarCuenta(c.idcuen, _AdminContexto)
                              }).ToList();
            ViewData["datosTabla5"] = datostabla;
            ViewBag.Meser = noMesero;
            return View();
        }
        //Interno a tablaMesero
        public IActionResult formularioMesero(String nombre)
        {
            if (nombre == null) {
                nombre = "";
            }
            noMesero = nombre;
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
        private static double SumarVenta(int id, AdminContext Context)
        {
            double? suma = (from e in Context.DetalleVentas
                           where e.id_venta == id 
                           select e.precio).ToList().Sum();
            double darsuma;
            darsuma = suma == null ? 0: (double)suma;
            return darsuma;
        }
        private static double SumarCuenta(int id, AdminContext Context)
        {
            double? suma = (double?) (from e in Context.DetalleCuentas
                            where e.id_cuenta == id && e.estado.Equals("CANCELADO")
                            select e.precio * (1 - e.descuento ) ).ToList().Sum();
            double darsuma;
            darsuma = suma == null ? 0 : (double)suma;
            return darsuma;
        }
        private DateTime ObtenerFechaHoy()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaMinima = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 0, 0, 0);

            return fechaMinima;
        }
    }
}
