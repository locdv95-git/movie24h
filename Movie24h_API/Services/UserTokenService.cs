using Movie24h_API.Models;
using Movie24h_API.Repositories.UserToken;

namespace Movie24h_API.Services {
    public class UserTokenService {

        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenService(IUserTokenRepository userTokenRepository) {
            _userTokenRepository = userTokenRepository;
        }

        public async Task<UserToken> GetUserTokenByRefreshTokenAsync(string refreshToken) {
            return await _userTokenRepository.GetUserTokenByRefreshTokenAsync(refreshToken);
        }

        //public async Task AddUserTokenAsync(UserToken userToken) {
        //    await _userTokenRepository.AddUserTokenAsync(userToken);
        //}

        //public async Task ModUserTokenAsync(UserToken userToken) {
        //    await _userTokenRepository.ModUserTokenAsync(userToken);
        //}

        //public async Task DelUserTokenAsync(UserToken userToken) {
        //    await _userTokenRepository.DelUserTokenAsync(userToken);
        //}

        //public async Task DelAllUserTokenNotCurrentUserTokenAsync(UserToken userToken) {
        //    await _userTokenRepository.DelAllUserTokenNotCurrentUserTokenAsync(userToken);
        //}

        public async Task SaveRefreshTokenToDB(string userName, string refreshToken) {
            var userToken = await _userTokenRepository.GetUserTokenByRefreshTokenAsync(refreshToken);

            if(userToken != null) {
                userToken.RefreshToken = refreshToken;
                userToken.Expiration = DateTime.UtcNow.AddDays(7);
                userToken.CreateDate = DateTime.UtcNow;

                await _userTokenRepository.ModUserTokenAsync(userToken);
            }
            else {
                userToken = new UserToken() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName,
                    RefreshToken = refreshToken,
                    Expiration = DateTime.UtcNow.AddDays(7),
                    DeviceInfo = "",
                    IP = "",
                    CreateDate = DateTime.UtcNow
                };

                await _userTokenRepository.AddUserTokenAsync(userToken);
            }
        }
    }
}
