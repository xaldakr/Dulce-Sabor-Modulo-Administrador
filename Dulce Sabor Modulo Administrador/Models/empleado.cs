using System.ComponentModel.DataAnnotations;

namespace Dulce_Sabor_Modulo_Administrador.Models
{
    public class empleado
    {
        [Key]
        public int id { get; set; }
        [StringLength(50)]
        public string? nombre { get; set; }
    }
}
