using Agap.Shared.Entities.Agap.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap.Shared.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Cultivo ID")]
        public int CropTypeId { get; set; }

        public CropType? CropType { get; set; }

        [Display(Name = "Proyecto")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public ProjectStatus Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Municipio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Municipality { get; set; } = null!;

        [Display(Name = "Lista de Cultivos")]
        //public List<Crop> CropList { get; set; } = new List<Crop>();
        public ICollection<Crop>? CropList { get; set; }


        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float TotalBudget { get; set; }

        public static implicit operator Project(ProjectReport v)
        {
            throw new NotImplementedException();
        }
    }
}

public enum ProjectStatus
{
    [Display(Name = "Creado")]
    Creado,

    [Display(Name = "Cerrado")]
    Cerrado
}