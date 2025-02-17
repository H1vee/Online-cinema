using System;

namespace Cinema.Core.DTOs
{
    public class ShowtimeDTO
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty; 
        public DateTime ShowDateTime { get; set; } 
        public TimeSpan StartTime { get; set; }
        public string HallName { get; set; } = string.Empty; 
    }
}

