using System.Collections.Generic;

namespace Cinema.Core.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Movies { get; set; } = new();
    }
}