using System.ComponentModel.DataAnnotations;

namespace Agap.Shared.Entities
{
    public class CropReport
    {
        public int Id { get; set; }

        [Display(Name = "Cultivo ID")]
        public int CropId { get; set; }

        public Crop? Crop { get; set; }

        [Display(Name = "Venta Total")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float TotalSale { get; set; }

        [Display(Name = "Presupuesto Asignado")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float AssignedBudget { get; set; }

        [Display(Name = "Gasto Esperado")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float ExpectedExpense { get; set; }

        [Display(Name = "Gasto Real")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float RealExpense { get; set; }

        [Display(Name = "Ganancia")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float Profit { get; set; }

        [Display(Name = "Rentabilidad")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float Profitability { get; set; }
    }
}
