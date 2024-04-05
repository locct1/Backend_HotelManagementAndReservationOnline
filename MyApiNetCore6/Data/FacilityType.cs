using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("FacilityType")]
    public class FacilityType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Icon { get; set; }
        public List<Facility> Facilities { get; set; }

    }
}
