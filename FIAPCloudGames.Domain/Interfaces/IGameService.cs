using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IGameService : ICreate<Game>, IUpdate<Game>, IFind<Game>, 
        IFindAll<Game>, IDelete<Game>
    {
    }
}
