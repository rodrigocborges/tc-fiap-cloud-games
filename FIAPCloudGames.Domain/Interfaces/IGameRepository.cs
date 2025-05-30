﻿using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Interfaces
{
    public interface IGameRepository : ICreate<Game>, IUpdate<Game>, IFind<Game>, 
        IFindAll<Game>, IDelete<Game>
    {
    }
}
