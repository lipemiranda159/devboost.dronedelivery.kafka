using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.felipe.Security.Entities;
using devboost.dronedelivery.security.domain.Entities;
using devboost.dronedelivery.security.domain.Interfaces;
using devboost.dronedelivery.security.service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Security_Login
{
    public class AccessManagerTests
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

        public AccessManagerTests()
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
        }

        [Fact]
        public async Task TestAccessManagerLoginSuccess()
        {
            var accessManager = new AccessManager(_signingConfigurations, _tokenConfigurations, _loginValidator);
            _loginValidator.GetUserById(Arg.Any<string>()).Returns(new ApplicationUser());
            _loginValidator.CheckPasswordUserAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
            _loginValidator.ValidateRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
            var valid = await accessManager.ValidateCredentials(SetupTests.GetLogin());
            Assert.True(valid);
        }

        [Fact]
        public void GenerateToken()
        {
            _tokenConfigurations.Audience = "ExemploAudience";
            _tokenConfigurations.Issuer = "ExemploIssuer";
            _tokenConfigurations.Seconds = 90;

            var accessManager = new AccessManager(_signingConfigurations, _tokenConfigurations, _loginValidator);
            var token = accessManager.GenerateToken(SetupTests.GetLogin());

            Assert.True(token.Authenticated);
            Assert.IsType<string>(token.AccessToken);
            Assert.IsType<Token>(token);

        }

        [Fact]
        public async Task CheckPasswordUserAsync()
        {
            var user = new ApplicationUser();
            var contextAccessor = Substitute.For<IHttpContextAccessor>();
            var claimsFactory = Substitute.For<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var logger = Substitute.For<ILogger<SignInManager<ApplicationUser>>>();
            var schemes = Substitute.For<IAuthenticationSchemeProvider>();
            var confirmation = Substitute.For<IUserConfirmation<ApplicationUser>>();
            var signInManager = Substitute.For<SignInManager<ApplicationUser>>(_userManager, contextAccessor,
                claimsFactory, _optionsAccessor, logger, schemes, confirmation);
            var loginValidator = new LoginValidator(signInManager, _userManager);
            signInManager.CheckPasswordSignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(SignInResult.Success);
            await loginValidator.CheckPasswordUserAsync(user, "");

            await signInManager.Received().CheckPasswordSignInAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<bool>());

        }

        [Fact]
        public async Task GetUserById()
        {
            var contextAccessor = Substitute.For<IHttpContextAccessor>();
            var claimsFactory = Substitute.For<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var logger = Substitute.For<ILogger<SignInManager<ApplicationUser>>>();
            var schemes = Substitute.For<IAuthenticationSchemeProvider>();
            var confirmation = Substitute.For<IUserConfirmation<ApplicationUser>>();
            var signInManager = Substitute.For<SignInManager<ApplicationUser>>(_userManager, contextAccessor,
                claimsFactory, _optionsAccessor, logger, schemes, confirmation);
            var loginValidator = new LoginValidator(signInManager, _userManager);

            await loginValidator.GetUserById("admin");

            await _userManager.Received().FindByNameAsync(Arg.Any<string>());
        }

        [Fact]
        public async Task ValidateRoleAsnc()
        {
            var contextAccessor = Substitute.For<IHttpContextAccessor>();
            var claimsFactory = Substitute.For<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var logger = Substitute.For<ILogger<SignInManager<ApplicationUser>>>();
            var schemes = Substitute.For<IAuthenticationSchemeProvider>();
            var confirmation = Substitute.For<IUserConfirmation<ApplicationUser>>();
            var signInManager = Substitute.For<SignInManager<ApplicationUser>>(_userManager, contextAccessor,
                claimsFactory, _optionsAccessor, logger, schemes, confirmation);
            var loginValidator = new LoginValidator(signInManager, _userManager);
            var user = new ApplicationUser();


            await loginValidator.ValidateRoleAsync(user, "admin");


            await _userManager.Received().IsInRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
        }
    }
}
