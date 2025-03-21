using Microsoft.EntityFrameworkCore;
using Movie24h_API.Data;

namespace Movie24h_API.Repositories.User {
    public class UserRepository : IUserRepository {

        private readonly Movie24hContext _context;

        public UserRepository(Movie24hContext context) {
            _context = context;
        }
        public async Task<IEnumerable<Models.User>> GetAllUserAsync() {
            return await _context.Users.ToListAsync();
        }

        public async Task<Models.User> GetUserByUsernameAsync(string username) {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task AddUserAsync(Models.User user) {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Models.User user) {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Models.User user) {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
