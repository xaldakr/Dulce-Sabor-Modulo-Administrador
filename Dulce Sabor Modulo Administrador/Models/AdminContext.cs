using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Dulce_Sabor_Modulo_Administrador.Models
{
    public class AdminContext:DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        //public DbSet<cuenta> Cuentas { get; set; }
        //public DbSet<detallecuenta> DetalleCuentas { get; set; }
    }
}

