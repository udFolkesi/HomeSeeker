using Core.Entities;
using DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AnnouncementRepository : IBaseRepository<Announcement>
    {
        private HomeSeekerDbContext _dbContext;
        public AnnouncementRepository(HomeSeekerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Announcement entity)
        {
            var courses = await GetAll();

/*            if (courses.Any(c => c.Name == entity.Name))
            {
                return false;
            }*/

            await _dbContext.Announcements.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Announcement>> GetAll()
        {
            var announcements = _dbContext.Announcements
                .Include(a => a.Images)
                .Include(a => a.Description)
                .Include(a => a.Category)
                .ToListAsync();

            return await announcements;
        }

        public async Task<Announcement> GetById(int id)
        {
            return await _dbContext.Announcements
                .Include(u => u.Category)
                .Include(u => u.Description)
                .Include(u => u.Images)
                .Include(u => u.Profile)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<bool> Update(Announcement entity)
        {
            throw new NotImplementedException();
        }
    }
}
