using System;
using System.Collections.Generic;

namespace Cinema.Core.DTOs
{
    public class CreateMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public float? Rating { get; set; }
        public TimeSpan Duration { get; set; }
        public string TrailerURL { get; set; } = string.Empty;
        public string? PosterURL { get; set; }
        public List<int> GenresIds { get; set; } = new(); 
        public List<ActorRoleDTO> Actors { get; set; } = new();
    }
}