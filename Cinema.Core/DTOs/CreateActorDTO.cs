using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.DTOs
{
    public class CreateActorDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        [MaxLength(100)]
        public string Nationality { get; set; } = string.Empty;
    }
}