using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Frequency_Type")]
    public class FrequencyType
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Frequency { get; set; }

    }
}
