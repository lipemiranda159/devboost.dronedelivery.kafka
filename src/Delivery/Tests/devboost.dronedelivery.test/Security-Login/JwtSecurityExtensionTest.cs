using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.felipe.Security.Extensions;
using devboost.dronedelivery.security.domain.Entities;
using devboost.dronedelivery.security.domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace devboost.dronedelivery.test.Security_Login
{
    public class JwtSecurityExtensionTest
    {
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly ILoginValidator _loginValidator;
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
        private readonly IServiceCollection _serviceColletion;

        public JwtSecurityExtensionTest()
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
            _userManager = Substitute.For<UserManager<ApplicationUser>>(_store, _optionsAccessor,
                _passwordHasher, _userValidators, _passwordValidators, _keyNormalizer, _errors, _services, _logger);
            _signingConfigurations = Substitute.For<SigningConfigurations>();
            _tokenConfigurations = Substitute.For<TokenConfigurations>();
            _loginValidator = Substitute.For<ILoginValidator>();
            _serviceColletion = Substitute.For<IServiceCollection>();
        }

        [Fact]
        public void TestClassJwtSecurityExtension()
        {
            JwtSecurityExtension.AddJwtSecurity(_serviceColletion, _signingConfigurations, _tokenConfigurations);
            Assert.True(true);
        }


    }
}
