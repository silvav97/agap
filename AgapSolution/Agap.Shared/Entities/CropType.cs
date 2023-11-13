namespace Agap.Shared.Entities
{
    using System.ComponentModel.DataAnnotations;

    namespace Agap.Shared.Entities
    {
        public class CropType
        {
            public int Id { get; set; }

            [Display(Name = "Clima")]
            [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
            public string Weather { get; set; } = null!;

            [Display(Name = "Nombre")]
            [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]
            public string Name { get; set; } = null!;

            [Display(Name = "Cantidad de plantas por metro cuadrado")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int PlantQuantityPerSquareMeter { get; set; }

            [Display(Name = "Tiempo de cosecha")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int HarvestTime { get; set; }

            [Display(Name = "Id de fertilizante")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int FertilizerId { get; set; }

            [Display(Name = "Cantidad de fertilizante por planta")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int FertilizerQuantityPerPlant { get; set; }

            [Display(Name = "Frecuencia de fertilizante")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int FertilizerFrequency { get; set; }

            [Display(Name = "Id de pesticida")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int PesticideId { get; set; }

            [Display(Name = "Cantidad de pesticida por planta")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int PesticideQuantityPerPlant { get; set; }

            [Display(Name = "Frecuencia de pesticida")]
            [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
            public int PesticideFrequency { get; set; }

            
            public Fertilizer? Fertilizer { get; set; }
            public Pesticide? Pesticide { get; set; }
        }
    }

}