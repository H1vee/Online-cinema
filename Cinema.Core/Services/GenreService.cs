using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            var genres = await _unitOfWork.Genres.GetAllWithMoviesAsync();
            return _mapper.Map<IEnumerable<GenreDTO>>(genres);
        }

        public async Task<GenreDTO?> GetGenreByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            return genre is null ? null : _mapper.Map<GenreDTO>(genre);
        }

        public async Task AddGenreAsync(CreateGenreDTO genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre is null) return false;

            _unitOfWork.Genres.Remove(genre);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}