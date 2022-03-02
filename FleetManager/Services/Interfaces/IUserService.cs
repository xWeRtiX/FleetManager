using FleetManager.Data;

namespace FleetManager.Services.Interfaces
{
    public interface IUserService
    {
        List<AppUser> GetUsers();

        Task<AppUser> GetUser(int id);

        Task Update(AppUser user);

        Task Delete(int id);
    }
}
