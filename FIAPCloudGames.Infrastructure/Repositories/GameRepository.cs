using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Interfaces;
using FIAPCloudGames.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;
        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Game model)
        {
            await _context.Games.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task Delete(Guid id)
        {
            Game? model = await Find(id);
            if (model == null)
                return;
            _context.Games.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> Find(Guid id)
            => await _context.Games.FindAsync(id);

        public async Task<ICollection<Game>?> FindAll(int skip = 0, int take = 10)
            => await _context.Games.Skip(skip).Take(take).ToListAsync();

        public async Task Update(Game model)
        {
            _context.Games.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
