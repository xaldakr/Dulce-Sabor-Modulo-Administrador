namespace Dulce_Sabor_Modulo_Administrador.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class detallecuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_detalle_cuenta { get; set; }

        [Required]
        public int id_cuenta { get; set; }

        public int? id_plato { get; set; }

        public int? id_combo { get; set; }

        public int? id_promocion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal precio { get; set; }

        [Range(0, 1)]
        public decimal descuento { get; set; }

        public string comentario { get; set; }

        [Required]
        [StringLength(20)]
        public string estado { get; set; }
    }
}
