using System.ComponentModel.DataAnnotations;

namespace Currency_Exchange_Manager_App.Model
{
    public class CurrencyInfo
    {
        [Key]
        public int Idcurrency_info { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Currency_details { get; set; }
        [Required]
        public int Rates { get; set; }

    }
}