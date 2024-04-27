using System.ComponentModel.DataAnnotations;

namespace Dulce_Sabor_Modulo_Administrador.Models
{
    public class mesa
    {
        [Key]
        public int id { get; set; }
        public int? numero_mesa { get; set; }
        public bool estado_mesa { get; set; }
    }
}
