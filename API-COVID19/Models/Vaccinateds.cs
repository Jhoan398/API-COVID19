using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_COVID19.Models
{
    [Table("Vaccinateds")]
    public class Vaccinateds
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Dosis { get; set; }
        public decimal AtLeastOneDosis { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country CountryCase { get; set; }
        
    }
}
