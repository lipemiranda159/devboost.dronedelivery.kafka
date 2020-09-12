using System.Threading.Tasks;

namespace devboost.dronedelivery.security.domain.Interfaces
{
    public interface IDroneRoleValidator
    {
        Task<bool> ExistRoleAsync(string role);
        Task<bool> CreateRoleAsync(string role);
    }
}
