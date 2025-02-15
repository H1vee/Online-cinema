using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorDTO>> GetAllActorsAsync()
        {
            var actors = await _unitOfWork.Actors.GetAllWithMoviesAsync();
            return _mapper.Map<IEnumerable<ActorDTO>>(actors);
        }

        public async Task<ActorDTO?> GetActorByIdAsync(int id)
        {
            var actor = await _unitOfWork.Actors.GetByIdAsync(id);
            return actor is null ? null : _mapper.Map<ActorDTO>(actor);
        }

        public async Task AddActorAsync(CreateActorDTO actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            await _unitOfWork.Actors.AddAsync(actor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteActorAsync(int id)
        {
            var actor = await _unitOfWork.Actors.GetByIdAsync(id);
            if (actor is null) return false;

            _unitOfWork.Actors.Remove(actor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}