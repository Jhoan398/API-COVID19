using API_COVID19.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_COVID19.Models
{
    [Table("Country")]
    public class Country
    {
        [Key, Required]
        public int Id { get; set; }
        public string Country_Name { get; set; } = String.Empty;
        public string Combined_Key { get; set; } = String.Empty;
        public virtual List<ProvinceState> ProvinceStates { get; set; } = new List<ProvinceState>();
    }
}
