namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IUserRepository<T> : ICreate<T>, IUpdate<T>, 
        IFind<T>, IFindAll<T>, IDelete<T> where T : IEntity
    {
    }
}
