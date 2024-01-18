using System.ComponentModel.DataAnnotations;

namespace Currency_Exchange_Manager_App.Model
{
    public class Convert_Currency
    {
        [Key]
        public int Idconvert_currency { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public double Conversion_Rate { get; set; }

        [Required]
        public string currency_from { get; set; }

        [Required]
        public string currency_to { get; set; }
    }
}
