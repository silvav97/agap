using System.ComponentModel.DataAnnotations;

namespace Agap.Shared.Entities
{
    public class Fertilizer
    {
        public int Id { get; set; }

        [Display(Name = "Fertilizante")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Marca")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Brand { get; set; } = null!;

        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float PricePerGram { get; set; }
    }
}
