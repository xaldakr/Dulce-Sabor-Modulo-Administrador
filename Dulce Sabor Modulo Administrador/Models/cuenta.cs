namespace Dulce_Sabor_Modulo_Administrador.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_cuenta { get; set; }

        [Required]
        public int id_mesa { get; set; }

        [Required]
        [StringLength(255)]
        public string nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string apellido { get; set; }

        public DateTime? fecha_apertura { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("^(ABIERTO|CERRADO)$")]
        public string estado { get; set; }


    }
}
