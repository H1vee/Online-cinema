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
            return _mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
        }

        public async Task<ShowtimeDTO?> GetShowtimeByIdAsync(int id)
        {
            var showtime = await _unitOfWork.Showtimes.GetByIdAsync(id);
            return showtime == null ? null : _mapper.Map<ShowtimeDTO>(showtime);
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetUpcomingShowtimesAsync()
        {
            var showtimes = await _unitOfWork.Showtimes.GetUpcomingShowtimes(); 
            return _mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
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
                HallID = showtimeDto.HallId
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