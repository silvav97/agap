using System.ComponentModel.DataAnnotations;

namespace Agap.Shared.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        [Display(Name = "ID del Cultivo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CropId { get; set; }

        public Crop? Crop { get; set; }

        [Display(Name = "Mensaje del Título")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string TitleMessage { get; set; } = null!;

        [Display(Name = "Mensaje del Cuerpo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string BodyMessage { get; set; } = null!;

        [Display(Name = "Periodicidad")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public int Periodicity { get; set; }
    }
}