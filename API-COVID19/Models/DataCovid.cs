using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Data_Covid")]
    public class DataCovid
    {
        [Key,Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Infected { get; set; }
        public decimal Deads { get; set; }
        public decimal Total_Cases { get; set; }
        public DateTime DateReport { get; set; }

        public int ProvinceStateId { get; set; }

        [ForeignKey("ProvinceStateId")]
        public virtual ProvinceState ProvinceState { get; set; }
        public int CountryId { get; set; }

        [ForeignKey ("CountryId")]
        public virtual Country Country { get; set; }
    }
}
  