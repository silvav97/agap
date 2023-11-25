using System.ComponentModel.DataAnnotations;

namespace Agap.Shared.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Cultivo ID")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        public int CropId { get; set; }

        public Crop? Crop { get; set; }

        [Display(Name = "Valor del Gasto")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float ExpenseValue { get; set; }

        [Display(Name = "Descripción del Gasto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public ExpenseType ExpenseDescription { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Fecha Gasto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime ExpenseDate { get; set; }
    }
}

public enum ExpenseType
{
    [Display(Name = "Gasto de Mano de Obra")]
    Trabajadores,

    [Display(Name = "Gasto de Pesticida")]
    Pesticida,

    [Display(Name = "Gasto de Fertilizante")]
    Fertilizante
}