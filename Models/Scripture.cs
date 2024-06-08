using System.ComponentModel.DataAnnotations;

namespace ScriptureJournal.Models
{
    public class Scripture
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Book { get; set; } = string.Empty;

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Reference { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date")]

        public DateTime DateAdded { get; set; }

        [Required]
        [StringLength(300)]
        public string Note { get; set; } = string.Empty;
    }
}