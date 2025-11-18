using CourseEntity = LMS_SoulCode.Features.Course.Entities.Course;
using LMS_SoulCode.Features.Course.Models;
using LMS_SoulCode.Features.Course.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using LMS_SoulCode.Features.CourseVideos.Entities;
using Microsoft.EntityFrameworkCore;
using LMS_SoulCode.Features.Course.Entities;
namespace LMS_SoulCode.Features.Course.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetAllAsync();
        Task AddAsync(CourseRequest request);
        Task<bool> UploadVideoAsync(int courseId, IFormFile file);
        Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(int courseId);
        Task<CourseEntity?> GetByIdAsync(int id);

    }
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IWebHostEnvironment _env;

        public CourseService(ICourseRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<CourseResponse>> GetAllAsync()
        {
            var courses = await _repository.GetAllAsync();
            return courses.Select(c => new CourseResponse
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Instructor = c.Instructor,
                Difficulty = c.Difficulty,
                DurationHours = c.DurationHours,
                Rating = c.Rating,
                Price = c.Price,
                IsActive = c.IsActive
            });
        }

        public async Task AddAsync(CourseRequest request)
        {
            var course = new CourseEntity
            {
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Instructor = request.Instructor,
                ThumbnailUrl = request.ThumbnailUrl,
                Difficulty = request.Difficulty,
                DurationHours = request.DurationHours,
                Price = request.Price,
                Lectures = request.Lectures,
                Materials = request.Materials,
                Tags = request.Tags
            };

            await _repository.AddAsync(course);
        }

        public async Task<bool> UploadVideoAsync(int courseId, IFormFile file)
        {
            var course = await _repository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception("Course not found");

            var rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folderPath = Path.Combine(rootPath, "uploads", "courses", courseId.ToString());
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var video = new CourseVideo
            {
                CourseId = courseId,
                Title = Path.GetFileNameWithoutExtension(file.FileName),
                VideoUrl = $"/uploads/courses/{courseId}/{fileName}"
            };

            await _repository.AddVideoAsync(video);
            //await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(int courseId)
             => await _repository.GetVideosByCourseIdAsync(courseId);
        
        public async Task<CourseEntity?> GetByIdAsync(int id)
           => await _repository.GetByIdAsync(id);

    }
}