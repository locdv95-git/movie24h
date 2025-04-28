using Microsoft.AspNetCore.Identity;
using Movie24h_API.DTOs;
using Movie24h_API.Helpers;
using Movie24h_API.Models;

namespace Movie24h_API.Services {
    public class AuthService {

        private readonly JwtHelper _jwtHelper;
        private readonly UserService _userService;
        private readonly UserTokenService _userTokenService;

        public AuthService(JwtHelper jwtHelper, UserService userService, UserTokenService userTokenService) {
            _jwtHelper = jwtHelper;
            _userService = userService;
            _userTokenService = userTokenService;
        }

        public async Task<AuthResultDTO> LoginAsync(string userName, string password) {
            var authResultDTO = new AuthResultDTO() { Success = false};
            var user = await _userService.GetUserByUsernameAsync(userName);

            if(user != null) {
                var hasher = new PasswordHasher<object>();
                var result = hasher.VerifyHashedPassword(null, user.Password, password);

                if(result == PasswordVerificationResult.Success) {
                    // Create accessToken & refreshToken
                    _jwtHelper.GenerateJwtToken(user, out string accessToken, out string refreshToken);

                    // Hash RefreshToken
                    string hashedRefreshToken = hasher.HashPassword(null, refreshToken);

                    // Save hashed RefreshToken to DB
                    await _userTokenService.SaveRefreshTokenToDB(user.UserName, hashedRefreshToken);

                    authResultDTO.Success = true;
                    authResultDTO.Message = "Login success";
                    authResultDTO.AccessToken = accessToken;
                    authResultDTO.RefreshToken = refreshToken;
                }
                else {
                    authResultDTO.Success = false;
                    authResultDTO.Message = "Invalid password";
                }
            }
            else {
                authResultDTO.Success = false;
                authResultDTO.Message = "User not found";
            }
            return authResultDTO;
        }

        public async Task<AuthResultDTO> RegisterAsync(RegisterDTO registerDto) {
            var authResultDTO = new AuthResultDTO() { Success = false};
            var user = await _userService.GetUserByUsernameAsync(registerDto.UserName);

            if(user != null) {
                authResultDTO.Message = "UserName existed";
            }
            else {
                // Hash password
                var hasher = new PasswordHasher<object>();
                string hashedPassword = hasher.HashPassword(null, registerDto.Password);

                var newUser = new User() {
                    UserName = registerDto.UserName,
                    Password = hashedPassword,
                    Active = true,
                    CreateDate = DateTime.UtcNow,
                    CreateById = ""
                };
                // Add user to DB
                await _userService.AddUserAsync(newUser);

                // Create accessToken & refreshToken
                _jwtHelper.GenerateJwtToken(newUser, out string accessToken, out string refreshToken);

                // hash Refresh Token
                string hashedRefreshToken = hasher.HashPassword(null, refreshToken);

                // Save hashed RefreshToken to DB
                await _userTokenService.SaveRefreshTokenToDB(newUser.UserName, hashedRefreshToken);

                authResultDTO.Success = true;
                authResultDTO.Message = "Register success";
                authResultDTO.AccessToken = accessToken;
                authResultDTO.RefreshToken = refreshToken;
            }
            return authResultDTO;
        }

        public async Task<AuthResultDTO> RefreshTokenAsync(string refreshToken) {
            var authResultDTO = new AuthResultDTO() { Success = false};

            // Hash RefreshToken
            var hasher = new PasswordHasher<object>();
            var hashedRefreshToken = hasher.HashPassword(null, refreshToken);
            var userToken = await _userTokenService.GetUserTokenByRefreshTokenAsync(hashedRefreshToken);

            if (userToken != null && userToken.Expiration > DateTime.UtcNow) {
                // Get user info
                var user = await _userService.GetUserByUsernameAsync(userToken.UserName);

                // Create new JwtToken
                _jwtHelper.GenerateJwtToken(user, out string newAccessToken, out string newRefreshToken);

                // Hash new RefreshToken
                hashedRefreshToken = hasher.HashPassword(null, newRefreshToken);

                // Save hashed RefreshToken to DB
                await _userTokenService.SaveRefreshTokenToDB(userToken.UserName, hashedRefreshToken);

                authResultDTO.Success = true;
                authResultDTO.Message = "Refresh Token success";
                authResultDTO.AccessToken = newAccessToken;
                authResultDTO.RefreshToken = newRefreshToken;
            }
            else if(userToken == null) {
                authResultDTO.Message = "Invalid Refresh Token";
            }
            else if(userToken.Expiration <= DateTime.UtcNow) {
                authResultDTO.Message = "Refresh Token has expired";
            }
            return authResultDTO;
        }
    }
}
