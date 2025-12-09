using CourseEntity = LMS_SoulCode.Features.Course.Models.Course;
using LMS_SoulCode.Features.Course.DTOs;
using LMS_SoulCode.Features.Course.Repositories;
using LMS_SoulCode.Features.CourseVideos.Models;
using LMS_SoulCode.Features.Common;
using Microsoft.AspNetCore.Http;
using LMS_SoulCode.Features.Security.Services;
using StatusCodes = LMS_SoulCode.Features.Common.StatusCodes;

namespace LMS_SoulCode.Features.Course.Services
{
    public interface ICourseService
    {
        Task<ApiResponse<IEnumerable<CourseResponse>>> GetAllAsync();
        Task<ApiResponse<CourseResponse>> GetByIdAsync(int id);
        Task<ApiResponse<CourseResponse>> AddAsync(CourseRequest request);
        Task<ApiResponse<IEnumerable<CourseResponse>>> GetCourseByCategoryIdAsync(int categoryId);
        Task<ApiResponse<string>> UploadVideoAsync(int courseId, IFormFile file);
        Task<ApiResponse<string>> UploadDocumentAsync(int courseId, IFormFile file);
        Task<ApiResponse<IEnumerable<CourseVideo>>> GetCourseVideosAsync(int courseId);
        Task<ApiResponse<IEnumerable<CourseDocument>>> GetCourseDocumentsAsync(int courseId);
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

        public async Task<ApiResponse<IEnumerable<CourseResponse>>> GetAllAsync()
        {
            var courses = await _repository.GetAllAsync();
            var response = courses.Select(c => new CourseResponse
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

            return ApiResponse<IEnumerable<CourseResponse>>.Success(response, Messages.Success);
        }

        public async Task<ApiResponse<CourseResponse>> GetByIdAsync(int id)
        {
            var course = await _repository.GetByIdAsync(id);

            if (course == null)
                return ApiResponse<CourseResponse>.Fail(Messages.NotFound, StatusCodes.NotFound);

            var response = new CourseResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Instructor = course.Instructor,
                Difficulty = course.Difficulty,
                DurationHours = course.DurationHours,
                Rating = course.Rating,
                Price = course.Price,
                IsActive = course.IsActive
            };

            return ApiResponse<CourseResponse>.Success(response, Messages.Success);
        }

        public async Task<ApiResponse<CourseResponse>> AddAsync(CourseRequest request)
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

            var response = new CourseResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Instructor = course.Instructor,
                Difficulty = course.Difficulty,
                DurationHours = course.DurationHours,
                Rating = course.Rating,
                Price = course.Price,
                IsActive = course.IsActive
            };

            return ApiResponse<CourseResponse>.Success(response, Messages.Created);
        }

        public async Task<ApiResponse<IEnumerable<CourseResponse>>> GetCourseByCategoryIdAsync(int categoryId)
        {
            var courses = await _repository.GetCoursesByCateIdAsync(categoryId);

            if (courses == null || !courses.Any())
                return ApiResponse<IEnumerable<CourseResponse>>.Fail("No courses found for this category", StatusCodes.NotFound);

            var response = courses.Select(c => new CourseResponse
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

            return ApiResponse<IEnumerable<CourseResponse>>.Success(response, Messages.Success);
        }

        public async Task<ApiResponse<string>> UploadVideoAsync(int courseId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return ApiResponse<string>.Fail("No file selected", StatusCodes.BadRequest);

            var course = await _repository.GetByIdAsync(courseId);
            if (course == null)
                return ApiResponse<string>.Fail(Messages.NotFound, StatusCodes.NotFound);

            var rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folderPath = Path.Combine(rootPath, "uploads", "courses", courseId.ToString());
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName) + ".enc";
            var filePath = Path.Combine(folderPath, fileName);

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            string encryptedBase64 = _crypto.EncryptBytes(fileBytes);
            await File.WriteAllTextAsync(filePath, encryptedBase64);

            var video = new CourseVideo
            {
                CourseId = courseId,
                Title = Path.GetFileNameWithoutExtension(file.FileName),
                VideoUrl = $"/uploads/courses/{courseId}/{fileName}"
            };

            await _repository.AddVideoAsync(video);

            return ApiResponse<string>.Success("Video uploaded successfully", Messages.Created);
        }

        public async Task<ApiResponse<string>> UploadDocumentAsync(int courseId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return ApiResponse<string>.Fail("No file selected", StatusCodes.BadRequest);

            var course = await _repository.GetByIdAsync(courseId);
            if (course == null)
                return ApiResponse<string>.Fail(Messages.NotFound, StatusCodes.NotFound);

            var rootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folderPath = Path.Combine(rootPath, "uploads", "courses", "docs", courseId.ToString());
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName) + ".enc";
            var filePath = Path.Combine(folderPath, fileName);

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            string encryptedBase64 = _crypto.EncryptBytes(fileBytes);
            await File.WriteAllTextAsync(filePath, encryptedBase64);

            var doc = new CourseDocument
            {
                CourseId = courseId,
                DocName = Path.GetFileNameWithoutExtension(file.FileName),
                DocUrl = $"/uploads/courses/docs/{courseId}/{fileName}"
            };

            await _repository.AddDocsAsync(doc);

            return ApiResponse<string>.Success("Document uploaded successfully", Messages.Created);
        }

        public async Task<ApiResponse<IEnumerable<CourseVideo>>> GetCourseVideosAsync(int courseId)
        {
            var videos = await _repository.GetVideosByCourseIdAsync(courseId);
            return ApiResponse<IEnumerable<CourseVideo>>.Success(videos, Messages.Success);
        }

        public async Task<ApiResponse<IEnumerable<CourseDocument>>> GetCourseDocumentsAsync(int courseId)
        {
            var docs = await _repository.GetDocsByCourseIdAsync(courseId);
            return ApiResponse<IEnumerable<CourseDocument>>.Success(docs, Messages.Success);
        }
    }
}
