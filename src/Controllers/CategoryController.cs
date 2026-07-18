using System.Security.Claims;
using to_do_list.src.Interfaces;
using to_do_list.src.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using to_do_list.src.Requests;
using Microsoft.Extensions.Primitives;

namespace to_do_list.src.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = new Dictionary<string, StringValues>(Request.Query);
            if (!string.IsNullOrEmpty(userId))
            {
                query["createdBy"] = userId;
            }
            ResponseApi<PaginationApi<List<dynamic>>> response = await service.GetAllAsync(new(Request.Query));
            return StatusCode(response.StatusCode, response.Result);
        }

        [Authorize]
        [HttpGet("select")]
        public async Task<IActionResult> GetSelect()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = new Dictionary<string, StringValues>(Request.Query);
            if (!string.IsNullOrEmpty(userId))
            {
                query["createdBy"] = userId;
            }
            ResponseApi<List<dynamic>> response = await service.GetSelectAsync(new(Request.Query));
            return StatusCode(response.StatusCode, response.Result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            ResponseApi<dynamic?> response = await service.GetByIdAggregateAsync(id);
            return StatusCode(response.StatusCode, response.Result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest task)
        {
            if (task == null) return BadRequest("Dados inválidos.");
            task.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            ResponseApi<to_do_list.src.Models.Category?> response = await service.CreateAsync(task);
            return StatusCode(response.StatusCode, response.Result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest task)
        {
            if (task == null) return BadRequest("Dados inválidos.");
            task.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            ResponseApi<to_do_list.src.Models.Category?> response = await service.UpdateAsync(task);
            return StatusCode(response.StatusCode, response.Result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ResponseApi<to_do_list.src.Models.Category> response = await service.DeleteAsync(new() { Id = id, DeletedBy = userId! });
            return StatusCode(response.StatusCode, response.Result);
        }
    }
}