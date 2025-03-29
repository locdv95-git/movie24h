using Microsoft.EntityFrameworkCore;
using Movie24h_API.Data;

namespace Movie24h_API.Repositories.UserToken {
    public class UserTokenRepository : IUserTokenRepository {

        private readonly Movie24hContext _context;

        public UserTokenRepository(Movie24hContext context) {
            _context = context;
        }

        public async Task<Models.UserToken> GetUserTokenByRefreshTokenAsync(string refreshToken) {
            return await _context.UserTokens.FirstOrDefaultAsync(ut => ut.RefreshToken == refreshToken);
        }

        public async Task AddUserTokenAsync(Models.UserToken userToken) {
            await _context.UserTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();
        }

        public async Task ModUserTokenAsync(Models.UserToken userToken) {
            _context.UserTokens.Update(userToken);
            await _context.SaveChangesAsync();
        }

        public async Task DelUserTokenAsync(Models.UserToken userToken) {
            _context.UserTokens.Remove(userToken);
            await _context.SaveChangesAsync();
        }

        public async Task DelAllUserTokenNotCurrentUserTokenAsync(Models.UserToken userToken) {
            var userTokens = await _context.UserTokens.Where(ut => ut.UserName == userToken.UserName && ut.RefreshToken != userToken.RefreshToken).ToListAsync();

            if (userTokens != null && userTokens.Count > 0)
            {
                _context.UserTokens.RemoveRange(userTokens);
                await _context.SaveChangesAsync();
            }
        }
    }
}
