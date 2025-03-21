namespace Movie24h_API.Repositories.User {
    public interface IUserRepository {
        Task<IEnumerable<Models.User>> GetAllUserAsync();
        Task<Models.User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(Models.User user);
        Task UpdateUserAsync(Models.User user);
        Task DeleteUserAsync(Models.User user);
    }
}
