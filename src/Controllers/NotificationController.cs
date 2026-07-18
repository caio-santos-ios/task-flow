using System.Security.Claims;
using to_do_list.src.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using to_do_list.src.Interfaces.INotification;

namespace to_do_list.src.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController(INotificationService service) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                Dictionary<string, StringValues> query = new(Request.Query);
                query["sendBy"] = userId;
                Request.Query = new QueryCollection(query);
            }
            ResponseApi<List<dynamic>> response = await service.GetAllAsync(new(Request.Query));
            return StatusCode(response.StatusCode, response.Result);
        }
    }
}