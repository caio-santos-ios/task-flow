using to_do_list.src.Interfaces;
using to_do_list.src.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace to_do_list.src.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController(IDashboardService service) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ResponseApi<dynamic> response = await service.GetAllAsync(userId!);
            return StatusCode(response.StatusCode, response.Result);
        }
    }
}