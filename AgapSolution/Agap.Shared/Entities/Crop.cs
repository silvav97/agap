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
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserId { get; set; } = null!;

        [Display(Name = "Proyecto ID")]
        public int ProjectId { get; set; }

        [Display(Name = "Tipo de Cultivo ID")]
        public int CropTypeId { get; set; }

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
        public List<Expense> ExpenseList { get; set; } = new List<Expense>();

        [Display(Name = "Valor de Venta")]
        public float? SaleValue { get; set; }

        [Display(Name = "Área")]
        public int Area { get; set; }
    }
}

public enum CropStatus
{
    [Display(Name = "Creado")]
    Created,

    [Display(Name = "En progreso")]
    InProgress,

    [Display(Name = "Cerrado")]
    Closed
}