using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Country_data_covid")]
    public class CountryDataCovid
    {
        [Key,Required]
        public int Id { get; set; }
        public decimal Infected { get; set; }
        public decimal Deads { get; set; }
        public decimal Total_Cases { get; set; }
        public int Vaccinations { get; set; }
        public DateTime DateReport { get; set; }
        public int CountryId { get; set; }
        
        [ForeignKey ("CountryId")]
        public virtual Country Country { get; set; }
    }
}
  