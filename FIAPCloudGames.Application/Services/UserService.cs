using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;

namespace FIAPCloudGames.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Create(User model)
            => await _repository.Create(model);

        public async Task Delete(Guid id)
            => await _repository.Delete(id);

        public async Task<User?> Find(Guid id)
            => await _repository.Find(id);

        public async Task<ICollection<User>?> FindAll(int skip = 0, int take = 10)
            => await _repository.FindAll(skip, take);

        public async Task Update(User model)
            => await _repository.Update(model);
    }
}
