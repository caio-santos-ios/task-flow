using to_do_list.src.Interfaces;
using to_do_list.src.Models.Base;
using Microsoft.AspNetCore.Mvc;
using to_do_list.src.Requests;
using to_do_list.src.Shared.Utils;

namespace to_do_list.src.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest body)
        {
            if (body == null) return BadRequest("Dados inválidos.");

            ResponseApi<dynamic?> response = await service.LoginAsync(body);
            return StatusCode(response.StatusCode, response.Result);
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (request == null) return BadRequest("Dados inválidos.");

            ResponseApi<dynamic?> response = await service.ForgotPasswordAsync(request);
            return StatusCode(response.StatusCode, response.Result);
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (request == null) return BadRequest("Dados inválidos.");

            ResponseApi<dynamic?> response = await service.ResetPasswordAsync(request);
            return StatusCode(response.StatusCode, response.Result);
        }
    }
}