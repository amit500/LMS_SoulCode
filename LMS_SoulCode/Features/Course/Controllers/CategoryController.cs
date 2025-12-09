using LMS_SoulCode.Features.Course.DTOs;
using LMS_SoulCode.Features.Course.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS_SoulCode.Features.Course.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
            => _categoryService = categoryService;

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            var response = await _categoryService.CreateAsync(request.CategoryName);

            // Return 201 Created
            return StatusCode(response.Code, response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request)
        {
            var response = await _categoryService.UpdateAsync(id, request.CategoryName);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
