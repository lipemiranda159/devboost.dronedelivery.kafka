using devboost.dronedelivery.felipe.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Security_Login
{
    public class DroneRoleValidatorTests
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole> _store;
        private readonly IEnumerable<IRoleValidator<IdentityRole>> _roleValidators;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IdentityErrorDescriber _errors;
        private readonly ILogger<RoleManager<IdentityRole>> _logger;
        public DroneRoleValidatorTests()
        {
            _store = Substitute.For<IRoleStore<IdentityRole>>();
            _roleValidators = Substitute.For<IEnumerable<IRoleValidator<IdentityRole>>>();
            _keyNormalizer = Substitute.For<ILookupNormalizer>();
            _errors = Substitute.For<IdentityErrorDescriber>();
            _logger = Substitute.For<ILogger<RoleManager<IdentityRole>>>();
            _roleManager = Substitute.For<RoleManager<IdentityRole>>(_store,
                _roleValidators,
                _keyNormalizer,
                _errors,
                _logger);

        }
        [Fact]
        public async Task CreateRoleTest()
        {
            var droneRoleValidator = new DroneRoleValidator(_roleManager);
            _roleManager.CreateAsync(Arg.Any<IdentityRole>()).Returns(new IdentityResult());
            await droneRoleValidator.CreateRoleAsync("teste");
        }
        [Fact]
        public async Task ExistRoleTest()
        {
            var droneRoleValidator = new DroneRoleValidator(_roleManager);
            _roleManager.RoleExistsAsync(Arg.Any<string>()).Returns(true);
            var exists = await droneRoleValidator.ExistRoleAsync("teste");
            Assert.True(exists);
        }
    }
}
