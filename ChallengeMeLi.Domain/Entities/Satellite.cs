using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Satellite
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SatelliteId { get; set; }

        [Key]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public decimal PosX { get; set; }

        [Required]
        public decimal PosY { get; set; }
    }
}