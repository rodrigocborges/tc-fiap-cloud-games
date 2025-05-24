using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IUserService : ICreate<User>, IUpdate<User>,
        IFind<User>, IFindAll<User>, IDelete<User>
    {
    }
}
