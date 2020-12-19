using System;

namespace API.Entities
{
    public class Property
    {
        public int Id { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool ForRent { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }
        
        public virtual AppUser User { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public int TypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }
    }
}