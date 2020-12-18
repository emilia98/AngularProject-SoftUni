using System;

namespace API.Entities
{
    public class Property
    {
        public int Id { get; set; }

        public int Bedrooms { get; set; }

        public decimal Price { get; set; }

        public string Decription { get; set; }

        public bool ForRent { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }
        
        public virtual AppUser User { get; set; }
    }
}