using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class Queja
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Reserva")]
        public int ReservaId { get; set; }

        [Required, ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [Required, ForeignKey("Servicio")]
        public int ServicioId { get; set; }

        [Required, StringLength(500)]
        public string Titulo { get; set; } = string.Empty;

        [Required, StringLength(2000)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        public int Calificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Estado { get; set; } = "Pendiente";

        public Reserva? Reserva { get; set; }
        public Cliente? Cliente { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
