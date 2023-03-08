using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Frecuency")]
    public class Frecuency
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Deaths { get; set; }
        public decimal Recovered { get; set; }
        public decimal Active { get; set; }
        public int DateReport { get; set; }
        public int CountryId { get; set; }
        public int FrecuencyTypeId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [ForeignKey("FrecuencyTypeId")]
        public FrecuencyType FrecuencyType { get; set; }

    }
}
