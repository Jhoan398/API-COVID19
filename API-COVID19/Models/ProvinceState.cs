using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Province_state")]
    public class ProvinceState
    {
        [Key]
        public int Id { get; set; }
        public string ProvinceName { get; set; } = String.Empty;
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

    }
}
