using CourseEntity = LMS_SoulCode.Features.Course.Models.Course;
using LMS_SoulCode.Features.Course.DTOs;
using LMS_SoulCode.Features.Course.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using LMS_SoulCode.Features.CourseVideos.Models;
using Microsoft.EntityFrameworkCore;
using LMS_SoulCode.Features.Course.DTOs;
using BCrypt.Net;
using LMS_SoulCode.Features.Security.Services;
namespace LMS_SoulCode.Features.Course.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetAllAsync();
        Task AddAsync(CourseRequest request);
        Task<bool> UploadVideoAsync(int courseId, IFormFile file);
        Task<bool> UploadDocumentAsync(int courseId, IFormFile file);
        Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(int courseId);
        Task<IEnumerable<CourseDocument>> GetCourseDocumentAsync(int courseId);
        Task<CourseEntity?> GetByIdAsync(int id);
        Task<List<CourseEntity>> GetCourseByCateIdAsync(int id);


    }
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IWebHostEnvironment _env;
        private readonly CryptographyService _crypto;

        public CourseService(ICourseRepository repository, IWebHostEnvironment env, CryptographyService crypto)
        {
            _repository = repository;
            _env = env;
            _crypto = crypto;
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

        //public async Task<bool> UploadVideoAsync(int courseId, IFormFile file)
        //{
        //    var course = await _repository.GetByIdAsync(courseId);
        //    if (course == null)
        //        throw new Exception("Course not found");

        //    var rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        //    var folderPath = Path.Combine(rootPath, "uploads", "courses", courseId.ToString());
        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //    var filePath = Path.Combine(folderPath, fileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    var video = new CourseVideo
        //    {
        //        CourseId = courseId,
        //        Title = Path.GetFileNameWithoutExtension(file.FileName),
        //        VideoUrl = $"/uploads/courses/{courseId}/{fileName}"
        //    };

        //    await _repository.AddVideoAsync(video);
        //    //await _repository.SaveChangesAsync();

        //    return true;
        //}

        public async Task<bool> UploadVideoAsync(int courseId, IFormFile file)
        {
            var course = await _repository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception("Course not found");

            var rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folderPath = Path.Combine(rootPath, "uploads", "courses", courseId.ToString());

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName) + ".enc";
            var filePath = Path.Combine(folderPath, fileName);

            // 1. Read file
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            // 2. Encrypt → Base64 (string)
            string encryptedBase64 = _crypto.EncryptBytes(fileBytes);

            // 3. Save encrypted as TEXT
            await File.WriteAllTextAsync(filePath, encryptedBase64);

            // 4. Save record
            var video = new CourseVideo
            {
                CourseId = courseId,
                Title = Path.GetFileNameWithoutExtension(file.FileName),
                VideoUrl = $"/uploads/courses/{courseId}/{fileName}"
            };

            await _repository.AddVideoAsync(video);

            return true;
        }

        public async Task<bool> UploadDocumentAsync(int courseId, IFormFile file)
        {
            var course = await _repository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception("Course not found");

            var rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folderPath = Path.Combine(rootPath, "uploads", "courses","docs", courseId.ToString());

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName) + ".enc";
            var filePath = Path.Combine(folderPath, fileName);

            // 1. Read file
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            // 2. Encrypt → Base64 (string)
            string encryptedBase64 = _crypto.EncryptBytes(fileBytes);

            // 3. Save encrypted as TEXT
            await File.WriteAllTextAsync(filePath, encryptedBase64);

            // 4. Save record
            var docs = new CourseDocument
            {
                CourseId = courseId,
                DocName = Path.GetFileNameWithoutExtension(file.FileName),
                DocUrl = $"/uploads/courses/docs/{courseId}/{fileName}"
            };

            await _repository.AddDocsAsync(docs);

            return true;
        }

        public async Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(int courseId)
             => await _repository.GetVideosByCourseIdAsync(courseId);
        public async Task<IEnumerable<CourseDocument>> GetCourseDocumentAsync(int courseId)
             => await _repository.GetDocsByCourseIdAsync(courseId);
        
        public async Task<CourseEntity?> GetByIdAsync(int id)
           => await _repository.GetByIdAsync(id);

        public async Task<List<CourseEntity>> GetCourseByCateIdAsync(int categoryId)
           => await _repository.GetCoursesByCateIdAsync(categoryId);
        
    }
}