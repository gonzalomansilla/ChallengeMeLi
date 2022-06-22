using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [Required]
        [ForeignKey("Name")]
        public Satellite Satellite { get; set; }

        public IEnumerable<Word> Words { get; set; }
    }
}