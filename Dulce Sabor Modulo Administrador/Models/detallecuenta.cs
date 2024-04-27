namespace Dulce_Sabor_Modulo_Administrador.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class detallecuenta
    {
        [Key]
        public int id_detalle_cuenta { get; set; }

        [Required]
        public int id_cuenta { get; set; }

        [Required]
        public decimal precio { get; set; }

        [Range(0, 1)]
        public decimal descuento { get; set; }

        [Required]
        [StringLength(20)]
        public string estado { get; set; }
    }
}
