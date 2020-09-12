using devboost.dronedelivery.security.domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Security
{
    public class DroneRoleValidator : IDroneRoleValidator
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public DroneRoleValidator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> CreateRoleAsync(string role)
        {
            var created = await _roleManager.CreateAsync(
                        new IdentityRole(role));
            return created.Succeeded;
        }

        public async Task<bool> ExistRoleAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}
