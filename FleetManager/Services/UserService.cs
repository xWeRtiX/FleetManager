using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }

        public async Task Update(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }


        public async Task<AppUser> GetUser(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public List<AppUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public async Task Delete(int id)
        {
            var userId = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(userId);
        }
    }
}
