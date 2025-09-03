using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAccess : ControllerBase
    {
        private IAuthClient _auth;

        public ClientAccess(IAuthClient auth)
        {
            _auth = auth;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginClient dto)
        {
            try
            {
                var result = await _auth.Login(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpClient dto) 
        {
            try
            {
                var result = await _auth.SignUp(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
