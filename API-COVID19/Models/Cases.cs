using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_COVID19.Models
{
    [Table("Cases")]
    public class Cases
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Deaths { get; set; }
        public decimal Confirmed { get; set; }
        public decimal Recovered { get; set; }
        public int CountryId { get; set; }
        public int? ProvinceStateId { get; set; } = null;

        public DateTime DateReport { get; set; }
        
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [JsonIgnore]
        [ForeignKey("ProvinceStateId")]
        public virtual ProvinceState ProvinceState { get; set; }
    }
}
