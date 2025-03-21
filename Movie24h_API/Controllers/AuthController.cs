using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie24h_API.DTOs;
using Movie24h_API.Services;

namespace Movie24h_API.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly AuthService _authService;

        public AuthController(AuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) {
            var result = new ResponseDTO<string>();

            if(string.IsNullOrEmpty(loginDTO.UserName?.Trim())) {
                result.Success = false;
                result.Message = "Username is required";
            }
            else if(string.IsNullOrEmpty(loginDTO.Password?.Trim())) {
                result.Success = false;
                result.Message = "Password is required";
            }
            else {
                result = await _authService.LoginAsync(loginDTO.UserName, loginDTO.Password);

                if(result.Success == true) {
                    return Ok(result);
                }
            }

            return Unauthorized(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO) {
            var result = new AuthResultDTO(){ Success = false};

            if(string.IsNullOrEmpty(registerDTO.UserName?.Trim())) {
                result.Message = "Username is required";
            }
            else if(string.IsNullOrEmpty(registerDTO.Password?.Trim())) {
                result.Message = "Password is required";
            }
            else if(registerDTO.Password.Length < 8) {
                result.Message = "Password must be at least 8 characters";
            }
            else {
                result = await _authService.RegisterAsync(registerDTO);

                if(result.Success == true) {
                    return Ok(result);
                }
            }

            return BadRequest(result);
        }
    }
}
