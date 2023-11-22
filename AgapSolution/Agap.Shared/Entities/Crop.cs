using System.ComponentModel.DataAnnotations;

namespace Agap.Shared.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Crop
    {
        public int Id { get; set; }

        [Display(Name = "Usuario ID")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserId { get; set; } = null!;

        public User? User { get; set; }

        [Display(Name = "Proyecto ID")]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        [Display(Name = "Cultivo")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public CropStatus Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Gasto Esperado")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float ExpectedExpense { get; set; }

        [Display(Name = "Presupuesto Asignado")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float AssignedBudget { get; set; }

        [Display(Name = "Lista de Gastos")]
        //public List<Expense> ExpenseList { get; set; } = new List<Expense>();
        public ICollection<Expense>? ExpenseList { get; set; }


        [Display(Name = "Valor de Venta")]
        public float? SaleValue { get; set; }

        [Display(Name = "Área")]
        public int Area { get; set; }
    }
}

public enum CropStatus
{
    [Display(Name = "Creado")]
    Creado,

    [Display(Name = "Activo")]
    Activo,

    [Display(Name = "Cerrado")]
    Cerrado
}