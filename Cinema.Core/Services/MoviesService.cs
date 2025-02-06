using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoviesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            return movie is null ? null : _mapper.Map<MovieDTO>(movie);
        }

        public async Task AddMovieAsync(MovieDTO movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie is null)
            {
                return false;
            }

            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesWithRatingAbove(float rating)
        {
            var movies = await _unitOfWork.Movies.GetMoviesWithRatingAbove(rating);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }
    }
}
