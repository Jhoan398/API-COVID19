using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country CountryCaseReport { get; set; }
        public int? ProvinceStateCaseId { get; set; }

        [ForeignKey("ProvinceStateId")]
        public virtual ProvinceState ProvinceState { get; set; }
    }
}
