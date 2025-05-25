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

        public Task<bool> Create(Announcement entity)
        {
            throw new NotImplementedException();
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

        public Task<Announcement> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Announcement entity)
        {
            throw new NotImplementedException();
        }
    }
}
