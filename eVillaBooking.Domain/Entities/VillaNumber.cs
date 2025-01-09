using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVillaBooking.Domain.Entities
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Villa_Number { get; set; }
        public string? SpecialDetails { get; set; }

        [ValidateNever]
        public Villa Villa { get; set; }

        [ForeignKey("Villa")]
        public int Villa_Id { get; set; }
    }
}
