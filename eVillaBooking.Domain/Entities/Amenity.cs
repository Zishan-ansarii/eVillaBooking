
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVillaBooking.Domain.Entities
{
    public class Amenity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        [ValidateNever]
        public Villa Villa { get; set; }

        [ForeignKey(nameof(Villa))]
        public int VillaId {  get; set; }

    }
}
