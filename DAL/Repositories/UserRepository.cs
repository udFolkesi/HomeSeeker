using Core.Entities;
using DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private HomeSeekerDbContext _dbContext;
        public UserRepository(HomeSeekerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<User>> GetAll()
        {
            var users = _dbContext.Users
                .Include(u => u.Profile)
                .Include(u => u.Profile.Favorites)
                .Include(u => u.Profile.Announcements)
                .Include(u => u.Profile.SenderMessages)
                .Include(u => u.Profile.ReceiverMessages)
                .ToListAsync();
            return await users;
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
