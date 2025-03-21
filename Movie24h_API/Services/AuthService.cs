using Microsoft.AspNetCore.Identity;
using Movie24h_API.DTOs;
using Movie24h_API.Helpers;
using Movie24h_API.Models;
using Movie24h_API.Repositories.User;

namespace Movie24h_API.Services {
    public class AuthService {

        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepository, JwtHelper jwtHelper) {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<ResponseDTO<string>> LoginAsync(string username, string password) {
            var responseDTO = new ResponseDTO<string>();
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if(user != null) {
                var passwordHasher = new PasswordHasher<object>();
                var result = passwordHasher.VerifyHashedPassword(null, user.Password, password);

                if(result == PasswordVerificationResult.Success) {
                    var tokenStr = _jwtHelper.GenerateJwtToken(user);

                    responseDTO.Success = true;
                    responseDTO.Message = "Login success";
                    responseDTO.Data = tokenStr;
                }
                else {
                    responseDTO.Success = false;
                    responseDTO.Message = "Password invalid";
                }
            }
            else {
                responseDTO.Success = false;
                responseDTO.Message = "User not found";
            }
            return responseDTO;
        }

        public async Task<AuthResultDTO> RegisterAsync(RegisterDTO registerDto) {
            var authResultDTO = new AuthResultDTO() { Success = false};
            var user = await _userRepository.GetUserByUsernameAsync(registerDto.UserName);

            if(user != null) {
                authResultDTO.Message = "UserName existed";
            }
            else {
                var passwordHasher = new PasswordHasher<object>();
                string hashedPassword = passwordHasher.HashPassword(null, registerDto.Password);

                var newUser = new User
                {
                    UserName = registerDto.UserName,
                    //Email = registerDto.Email,
                    Password = hashedPassword
                };

                await _userRepository.AddUserAsync(newUser);
                var tokenStr = _jwtHelper.GenerateJwtToken(newUser);

                authResultDTO.Success = true;
                authResultDTO.Message = "Register success";
                authResultDTO.Token = tokenStr;
            }
            return authResultDTO;
        }
    }
}
