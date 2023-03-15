using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Frequency")]
    public class Frequency
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Deaths { get; set; }
        public decimal Confirmed { get; set; }
        public decimal Recovered { get; set; }
        public DateTime DateReport { get; set; }
        public int CountryId { get; set; }
        public int FrequencyTypeId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [ForeignKey("FrequencyTypeId")]
        public FrequencyType FrequencyType { get; set; }

    }
}
