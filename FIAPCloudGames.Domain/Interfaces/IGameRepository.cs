namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IGameRepository<T> : ICreate<T>, IUpdate<T>, IFind<T>, 
        IFindAll<T>, IDelete<T> where T : IEntity
    {
    }
}
