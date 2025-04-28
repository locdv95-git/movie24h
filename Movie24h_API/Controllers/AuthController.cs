using Microsoft.AspNetCore.Mvc;
using Movie24h_API.DTOs;
using Movie24h_API.Services;

namespace Movie24h_API.Controllers {
    [Route("api/acount")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly AuthService _authService;

        public AuthController(AuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request) {
            var result = new AuthResultDTO(){ Success = false};

            if(string.IsNullOrEmpty(request.UserName?.Trim())) {
                result.Success = false;
                result.Message = "Username is required";
            }
            else if(string.IsNullOrEmpty(request.Password?.Trim())) {
                result.Success = false;
                result.Message = "Password is required";
            }
            else {
                result = await _authService.LoginAsync(request.UserName, request.Password);

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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] UserTokenDTO request) {
            var result = new AuthResultDTO(){ Success = false};

            if(string.IsNullOrEmpty(request.RefreshToken?.Trim())) {
                result.Message = "Refresh Token is required";
            }
            else {
                result = await _authService.RefreshTokenAsync(request.RefreshToken);

                if(result.Success == true) {
                    return Ok(result);
                }
            }

            return BadRequest(result);
        }
    }
}
