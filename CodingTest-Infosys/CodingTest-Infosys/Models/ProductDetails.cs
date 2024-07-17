using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CodingTest_Infosys.Models
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }

        [DataMember(Name = "FirstName")]
        [RegularExpression("([a-zA-Z0-9]+)",ErrorMessage ="Only AlphaNumerics allowed")]
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [DataMember(Name = "LastName")]
        [RegularExpression("([a-zA-Z0-9]+)", ErrorMessage = "Only AlphaNumerics allowed")]
        [MaxLength(20)]
        [Required]
        public string LastName { get; set; }

        [DataMember(Name = "Description")]
        [MaxLength(100)]
        [Required]
        [RegularExpression("([a-zA-Z0-9\\s]+)", ErrorMessage = "Only AlphaNumerics allowed")]
        public string Description { get; set; }

        [DataMember(Name = "Quantity")]
        [Range(1,20)]
        public int Quantity { get; set; }
    }
    
}
