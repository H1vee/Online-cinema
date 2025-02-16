using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            return movies.Select(m => new MovieDTO
            {
                Id = m.MovieID,
                Title = m.Title,
                Description = m.Description,
                ReleaseDate = m.ReleaseDate ?? DateTime.MinValue,
                Rating = m.Rating,
                Duration = m.Duration,
                Genres = m.MovieGenres?.Select(g => g.Genre.Name).ToList() ?? new List<string>(),
                Actors = m.MovieActors?.Select(a => a.Actor.Name).ToList() ?? new List<string>()
            }).ToList();
        }


        public async Task<MovieDTO?> GetMovieByIdAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie is null) return null;

            return new MovieDTO
            {
                Id = movie.MovieID,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate ?? DateTime.MinValue,
                Rating = movie.Rating,
                Duration = movie.Duration,
                Genres = movie.MovieGenres?.Select(g => g.Genre.Name).ToList() ?? new List<string>(),
                Actors = movie.MovieActors?.Select(a => a.Actor.Name).ToList() ?? new List<string>()
            };
        }


        public async Task AddMovieAsync(CreateMovieDTO movieDto)
        {
            var movie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate,
                Rating = movieDto.Rating,
                Duration = movieDto.Duration,
                TrailerURL = movieDto.TrailerURL,
                PosterURL = movieDto.PosterURL
            };

            
            var genres = await _unitOfWork.Genres.GetAllAsync();
            var selectedGenres = genres.Where(g => movieDto.GenresIds.Contains(g.GenreID)).ToList();
            movie.MovieGenres = selectedGenres.Select(g => new MovieGenre { GenreID = g.GenreID }).ToList();

            
            var actors = await _unitOfWork.Actors.GetAllAsync();
            var selectedActors = actors.Where(a => movieDto.ActorsIds.Contains(a.ActorID)).ToList();
            movie.MovieActors = selectedActors.Select(a => new MovieActor { ActorID = a.ActorID }).ToList();

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
            return movies.Select(m => new MovieDTO
            {
                Id = m.MovieID,
                Title = m.Title,
                Description = m.Description,
                ReleaseDate = m.ReleaseDate ?? DateTime.MinValue,
                Rating = m.Rating,
                Duration = m.Duration,
                Genres = m.MovieGenres?.Select(g => g.Genre.Name).ToList() ?? new List<string>(),
                Actors = m.MovieActors?.Select(a => a.Actor.Name).ToList() ?? new List<string>()
            }).ToList();
        }
    }
}
