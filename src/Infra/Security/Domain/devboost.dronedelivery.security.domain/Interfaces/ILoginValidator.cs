using devboost.dronedelivery.security.domain.Entities;
using System.Threading.Tasks;

namespace devboost.dronedelivery.security.domain.Interfaces
{
    public interface ILoginValidator
    {
        Task<bool> CheckPasswordUserAsync(ApplicationUser user, string password);
        Task<bool> ValidateRoleAsync(ApplicationUser user, string role);

        Task<ApplicationUser> GetUserById(string userId);
    }
}
