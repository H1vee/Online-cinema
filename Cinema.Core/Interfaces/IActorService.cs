using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDTO>> GetAllActorsAsync();
        Task<ActorDTO?> GetActorByIdAsync(int id);
        Task AddActorAsync(CreateActorDTO actorDto);
        Task<bool> DeleteActorAsync(int id);
    }
}