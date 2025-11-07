using LMS_SoulCode.Features.CourseVideos.Entities;

namespace LMS_SoulCode.Features.CourseVideos.Repositories
{
    public interface ICourseVideoRepository
    {
        Task<IEnumerable<CourseVideo>> GetByCourseIdAsync(int courseId);
<<<<<<< HEAD
        //Task GetByIdAsync(int videoId);
         //Task<IEnumerable<CourseVideo>> GetVideoUrlAsync(string videoId);
=======
>>>>>>> 695d9a84ec067e90d0561fc504f0ad01d6228d89
    }
}
