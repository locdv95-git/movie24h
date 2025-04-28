namespace Movie24h_API.Repositories.UserToken {
    public interface IUserTokenRepository {
        Task<Models.UserToken> GetUserTokenByRefreshTokenAsync(string refreshToken);
        Task AddUserTokenAsync(Models.UserToken userToken);
        Task ModUserTokenAsync(Models.UserToken userToken);
        Task DelUserTokenAsync(Models.UserToken userToken);
        Task DelAllUserTokenNotCurrentUserTokenAsync(Models.UserToken userToken);
    }
}
