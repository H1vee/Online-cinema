namespace Cinema.Core.DTOs;

public class CreateShowtimeDTO
{
    
    public int MovieId { get; set; }  
    public DateTime ShowDateTime { get; set; }  
    public int HallId { get; set; }  
}