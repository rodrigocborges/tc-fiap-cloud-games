using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Create(Game model)
            => await _repository.Create(model);

        public async Task Delete(Guid id)
            => await _repository.Delete(id);

        public async Task<Game?> Find(Guid id)
            => await _repository.Find(id);

        public async Task<ICollection<Game>?> FindAll(int skip = 0, int take = 10)
            => await _repository.FindAll(skip, take);

        public async Task Update(Game model)
            => await _repository.Update(model);
    }
}
