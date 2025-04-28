using Movie24h_API.Models;
using Movie24h_API.Repositories.User;

namespace Movie24h_API.Services {
    public class UserService {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByUsernameAsync(string userName) {
            return await _userRepository.GetUserByUsernameAsync(userName);
        }

        public async Task AddUserAsync(User user) {
            await _userRepository.AddUserAsync(user);
        }
    }
}
