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
    public class ImageRepository: IBaseRepository<Image>
    {
        private readonly HomeSeekerDbContext _dbContext;

        public ImageRepository(HomeSeekerDbContext context)
        {
            _dbContext = context;
        }
        public async Task AddRangeAsync(IEnumerable<Image> images)
        {
            await _dbContext.Images.AddRangeAsync(images);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Create(Image entity)
        {
            await _dbContext.Images.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            
            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Image>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
