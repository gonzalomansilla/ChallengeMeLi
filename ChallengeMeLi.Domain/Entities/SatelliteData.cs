using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class SatelliteData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SatelliteDataId { get; set; }

        [Required]
        public decimal Distance { get; set; }

        [Required]
        [ForeignKey("Name")]
        public Satellite Satellite { get; set; }
    }
}