using LMS_SoulCode.Features.Course.DTOs;
using LMS_SoulCode.Features.Course.Models;
using CreatePermissionRequest = LMS_SoulCode.Features.Course.DTOs.CategoryRequest;
using LMS_SoulCode.Features.Course.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using LMS_SoulCode.Features.Auth.Models;

namespace LMS_SoulCode.Features.Course.Controllers
{
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
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { message = "Category not found" });

            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            var category = await _categoryService.CreateAsync(request.CategoryName);
            var response = new CategoryResponse(category.Id, "Category created successfully!");

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request)
        {
            await _categoryService.UpdateAsync(id, request.CategoryName);
            var response = new CategoryResponse(id, "Category updated successfully!");

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            var response = new CategoryResponse(id, "Category deleted successfully!");

            return Ok(response);
        }
    }
}
