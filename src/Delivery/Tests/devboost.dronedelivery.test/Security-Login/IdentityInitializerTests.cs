using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.security.domain.Entities;
using devboost.dronedelivery.security.domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace devboost.dronedelivery.test.Security_Login
{
    public class IdentityInitializerTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _store;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IEnumerable<IUserValidator<ApplicationUser>> _userValidators;
        private readonly IEnumerable<IPasswordValidator<ApplicationUser>> _passwordValidators;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IdentityErrorDescriber _errors;
        private readonly IServiceProvider _services;
        private readonly ILogger<UserManager<ApplicationUser>> _logger;
        private readonly IDroneRoleValidator _droneRoleValidator;
        private readonly IValidateDatabase _validateDatabase;
        public IdentityInitializerTests()
        {
            _store = Substitute.For<IUserStore<ApplicationUser>>();
            _optionsAccessor = Substitute.For<IOptions<IdentityOptions>>();
            _passwordHasher = Substitute.For<IPasswordHasher<ApplicationUser>>();
            _userValidators = Substitute.For<IEnumerable<IUserValidator<ApplicationUser>>>();
            _passwordValidators = Substitute.For<IEnumerable<IPasswordValidator<ApplicationUser>>>();
            _keyNormalizer = Substitute.For<ILookupNormalizer>();
            _errors = Substitute.For<IdentityErrorDescriber>();
            _services = Substitute.For<IServiceProvider>();
            _logger = Substitute.For<ILogger<UserManager<ApplicationUser>>>();
            _userManager = Substitute.For<UserManager<ApplicationUser>>(_store,
                _optionsAccessor,
                _passwordHasher,
                _userValidators,
                _passwordValidators,
                _keyNormalizer,
                _errors,
                _services,
                _logger);
            _droneRoleValidator = Substitute.For<IDroneRoleValidator>();
            _validateDatabase = Substitute.For<IValidateDatabase>();
        }

        [Fact]
        public void InitializerTests()
        {
            var initializer = new IdentityInitializer(_validateDatabase, _userManager, _droneRoleValidator);
            _droneRoleValidator.CreateRoleAsync(Arg.Any<string>()).Returns(true);
            _droneRoleValidator.ExistRoleAsync(Arg.Any<string>()).Returns(true);
            _validateDatabase.EnsureCreated().Returns(true);
            initializer.Initialize();
            _userManager.Received().FindByNameAsync(Arg.Any<string>());
        }

        [Fact]
        public void InitializerFailedTests()
        {
            var initializer = new IdentityInitializer(_validateDatabase, _userManager, _droneRoleValidator);
            _droneRoleValidator.CreateRoleAsync(Arg.Any<string>()).Returns(false);
            _droneRoleValidator.ExistRoleAsync(Arg.Any<string>()).Returns(false);
            _validateDatabase.EnsureCreated().Returns(true);
            Assert.Throws<Exception>(() => initializer.Initialize());

        }

    }
}
