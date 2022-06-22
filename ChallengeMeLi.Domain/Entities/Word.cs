using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Word
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WordId { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        [MaxLength(50)]
        public string Text { get; set; }

        [Required]
        [ForeignKey("MessageId")]
        public Message Message { get; set; }
    }
}