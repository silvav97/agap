using System.ComponentModel.DataAnnotations;

namespace Agap.Shared.Entities
{
    public class ProjectReport
    {
        public int Id { get; set; }

        [Display(Name = "Proyecto ID")]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        [Display(Name = "Venta Total")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float TotalSale { get; set; }

        [Display(Name = "Presupuesto Total")]
        [Range(0.000001f, float.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
        public float TotalBudget { get; set; }

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
