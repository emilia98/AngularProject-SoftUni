using System.ComponentModel.DataAnnotations;

namespace API.Models.InputModels
{
    public class NewPropertyInputModel
    {
        [Required]
        public int? Location { get; set; }

        [Required]
        public int? Type { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Price should be in a value between 0 and 10000!")]
        public decimal? Price { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Bedrooms should be in a value between 0 and 20!")]
        public int? Bedrooms { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Bathrooms should be in a value between 0 and 20!")]
        public int? Bathrooms { get; set; }

        public string Description { get; set; }
    }
}