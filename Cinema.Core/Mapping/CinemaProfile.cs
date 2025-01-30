using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Infrastructure.Entities;

namespace Cinema.Core.Mapping
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MovieID))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre.Name).ToList()))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Actor.Name).ToList()));

            CreateMap<MovieDTO, Movie>()
                .ForMember(dest => dest.MovieID, opt => opt.MapFrom(src => src.Id));

            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TicketID))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Showtime.Movie.Title))
                .ForMember(dest => dest.ShowDateTime, opt => opt.MapFrom(src => src.Showtime.ShowDateTime))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<TicketDTO, Ticket>()
                .ForMember(dest => dest.TicketID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ShowtimeID, opt => opt.Ignore())
                .ForMember(dest => dest.SeatID, opt => opt.Ignore())
                .ForMember(dest => dest.UserID, opt => opt.Ignore()) 
                .ForMember(dest => dest.SaleID, opt => opt.Ignore()) 
                .ForMember(dest => dest.RuleID, opt => opt.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoleAssignment.Select(ura => ura.UserRole.RoleName).ToList()));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoleAssignments, opt => opt.Ignore());

        }
    }
}
