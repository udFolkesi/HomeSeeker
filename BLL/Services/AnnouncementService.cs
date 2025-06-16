using BLL.Services.Abstractions;
using Core.Entities;
using Core.Response;
using Core.Response.Abstractions;
using DAL.Repositories;
using DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class AnnouncementService : IService<Announcement>
    {
        private IBaseRepository<Announcement> _announcementRepository { get; set; }
        private IBaseRepository<Image> _imageRepository { get; set; }
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AnnouncementService(IBaseRepository<Announcement> repository, IWebHostEnvironment hostingEnvironment,
            IBaseRepository<Image> imageRepository)
        {
            _announcementRepository = repository;
            _hostingEnvironment = hostingEnvironment;
            _imageRepository = imageRepository;
        }

        public async Task<IBaseResponse<bool>> AddAsync(Announcement announcement)
        {
            IBaseResponse<bool> response = new BaseResponse<bool>();

            announcement.PublishedAt = DateTime.UtcNow;
            announcement.UpdatedAt = DateTime.UtcNow;

            if (await _announcementRepository.Create(announcement))
            {
                var imageList = new List<Image>();

                foreach (var file in announcement.ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var image = new Image
                        {
                            Url = "/images/" + uniqueFileName,
                            IsMain = false,
                            AnnouncementId = announcement.Id
                        };

                        imageList.Add(image);
                    }
                }

                if (imageList.Count > 0)
                {
                    foreach (var image in imageList)
                    {
                        await _imageRepository.Create(image);
                    }
                }

                //response.Data = announcement;
                response.Success = true;
            }
            else
            {
                response.Message = $"Error during ad creating";
                response.Success = false;
            }

            return response;
        }

        public Task<IBaseResponse<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<ICollection<Announcement>>> GetAllAsync()
        {
            IBaseResponse<ICollection<Announcement>> response = new BaseResponse<ICollection<Announcement>>();

            ICollection<Announcement> courses = await _announcementRepository.GetAll();

            if (courses.Count != 0)
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "Courses not found";
                response.Data = courses;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "Courses not found";
                response.Data = courses;
            }

            return response;
        }

        public async Task<IBaseResponse<Announcement>> GetByIdAsync(int id)
        {
            IBaseResponse<Announcement> response = new BaseResponse<Announcement>();

            Announcement announcement = await _announcementRepository.GetById(id);

            if (announcement is not null)
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "Announcement found";
                response.Data = announcement;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "Announcement not found";
                response.Data = null;
            }

            return response;
        }

        public Task<IBaseResponse<bool>> UpdateAsync(Announcement entity)
        {
            throw new NotImplementedException();
        }
    }
}
