using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.DTOs
{
    public class CreateGenreDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}