using CourseEntity = LMS_SoulCode.Features.Course.Entities.Course;
using LMS_SoulCode.Features.Course.Models;
using LMS_SoulCode.Features.Course.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LMS_SoulCode.Features.Course.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponse>> GetAllAsync();
        Task AddAsync(CourseRequest request);
    }
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService(ICourseRepository repository)
        {
            _repository = repository;
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
    }
}
