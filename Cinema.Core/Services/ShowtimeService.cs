using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShowtimeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync()
        {
            var showtimes = await _unitOfWork.Showtimes.GetAllAsync();
            var showtimeDtos = new List<ShowtimeDTO>();
            foreach (var showtime in showtimes)
            {
                var movie = await _unitOfWork.Movies.GetByIdAsync(showtime.MovieID);
                var hall = await _unitOfWork.Halls.GetByIdAsync(showtime.HallID);

                showtimeDtos.Add(new ShowtimeDTO
                {
                    Id = showtime.ShowtimeID,
                    MovieTitle = movie?.Title ?? "Unknown Movie",
                    ShowDateTime = showtime.ShowDateTime,
                    StartTime = showtime.StartTime,
                    HallName = hall?.Name ?? "Unknown Hall"
                });
            }
            
            return showtimeDtos;
        }

        public async Task<ShowtimeDTO?> GetShowtimeByIdAsync(int id)
        {
            var showtime = await _unitOfWork.Showtimes.GetByIdAsync(id);
            if (showtime == null) return null;

            var movie = await _unitOfWork.Movies.GetByIdAsync(showtime.MovieID);
            var hall = await _unitOfWork.Halls.GetByIdAsync(showtime.HallID);

            return new ShowtimeDTO
            {
                Id = showtime.ShowtimeID,
                MovieTitle = movie?.Title ?? "Unknown Movie",
                ShowDateTime = showtime.ShowDateTime,
                StartTime = showtime.StartTime,
                HallName = hall?.Name ?? "Unknown Hall"
            };
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            var showtimes = await _unitOfWork.Showtimes.GetShowtimesByDate(date);
            return _mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByGenreAsync(string genreName)
        {
            var showtimes = await _unitOfWork.Showtimes.GetShowtimesByGenre(genreName);
            return _mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDurationAsync(int maxDuration)
        {
            var showtimes = await _unitOfWork.Showtimes.GetShowtimesByDurationAsync(maxDuration);

            return showtimes.Select(showtime => new ShowtimeDTO
            {
                Id = showtime.ShowtimeID,
                MovieTitle = showtime.Movie.Title,
                ShowDateTime = showtime.ShowDateTime,
                StartTime = showtime.StartTime,
                HallName = showtime.Hall?.Name ?? "Unknown Hall"
            }).ToList();
        }

        public async Task AddShowtimeAsync(CreateShowtimeDTO showtimeDto)
        {
            
            var movieExists = await _unitOfWork.Movies.GetByIdAsync(showtimeDto.MovieId);
            if (movieExists == null)
            {
                throw new Exception($"Movie with ID {showtimeDto.MovieId} does not exist.");
            }

            var showtime = new Showtime
            {
                MovieID = showtimeDto.MovieId,
                ShowDateTime = showtimeDto.ShowDateTime,
                HallID = showtimeDto.HallId,
                StartTime = showtimeDto.StartTime
            };

            await _unitOfWork.Showtimes.AddAsync(showtime);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteShowtimeAsync(int id)
        {
            var showtime = await _unitOfWork.Showtimes.GetByIdAsync(id);
            if (showtime == null) return false;

            _unitOfWork.Showtimes.Remove(showtime);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}