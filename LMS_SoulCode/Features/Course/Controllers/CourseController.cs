using LMS_SoulCode.Features.Course.DTOs;
using LMS_SoulCode.Features.Course.Services;
using LMS_SoulCode.Features.UserPermissions.PermissionHandler;
using LMS_SoulCode.Features.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = LMS_SoulCode.Features.Common.StatusCodes;

namespace LMS_SoulCode.Features.Course.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly PermissionHelper _permissionHelper;

        public CourseController(ICourseService courseService, PermissionHelper permissionHelper)
        {
            _courseService = courseService;
            _permissionHelper = permissionHelper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var response = await _courseService.GetCourseByCategoryIdAsync(categoryId);
            return StatusCode(response.Code, response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CourseRequest request)
        {
            var response = await _courseService.AddAsync(request);
            return StatusCode(response.Code, response);
        }

        [HttpPost("{courseId}/upload-video")]
        public async Task<IActionResult> UploadVideo(int courseId, IFormFile file)
        {
            var response = await _courseService.UploadVideoAsync(courseId, file);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{courseId}/videos")]
        public async Task<IActionResult> GetVideos(int courseId)
        {
            var response = await _courseService.GetCourseVideosAsync(courseId);
            return StatusCode(response.Code, response);
        }

        [HttpPost("{courseId}/upload-document")]
        public async Task<IActionResult> UploadDocument(int courseId, IFormFile file)
        {
            var response = await _courseService.UploadDocumentAsync(courseId, file);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{courseId}/documents")]
        public async Task<IActionResult> GetDocuments(int courseId)
        {
            var response = await _courseService.GetCourseDocumentsAsync(courseId);
            return StatusCode(response.Code, response);
        }
    }
}
