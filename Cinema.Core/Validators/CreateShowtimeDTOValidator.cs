using FluentValidation;
using Cinema.Core.DTOs;
namespace Cinema.Core.Validators
{
  public class CreateShowtimeDTOValidator:AbstractValidator<CreateShowtimeDTO>
  {
      public CreateShowtimeDTOValidator()
      {
          RuleFor(showtime => showtime.MovieId)
              .GreaterThan(0).WithMessage("MovieId must be greater than 0");

          RuleFor(showtime => showtime.ShowDateTime)
              .GreaterThan(DateTime.UtcNow).WithMessage("Showtime must be in the future");

          RuleFor(showtime => showtime.HallId)
              .GreaterThan(0).WithMessage("HallId must be greater than 0");
      }
      }
  }  

