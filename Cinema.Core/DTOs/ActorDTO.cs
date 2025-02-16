using System;
using System.Collections.Generic;

namespace Cinema.Core.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public List<string> Movies { get; set; } = new();
    }
}