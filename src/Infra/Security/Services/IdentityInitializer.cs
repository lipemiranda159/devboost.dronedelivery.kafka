using devboost.dronedelivery.felipe.Security.Entities;
using devboost.dronedelivery.security.domain.Entities;
using devboost.dronedelivery.security.domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace devboost.dronedelivery.felipe.Security
{
    public class IdentityInitializer
    {
        private readonly IValidateDatabase _validateDatabase;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDroneRoleValidator _droneRoleValidator;
        public IdentityInitializer(
            IValidateDatabase validateDatabase,
            UserManager<ApplicationUser> userManager,
            IDroneRoleValidator droneRoleValidator)
        {
            _validateDatabase = validateDatabase;
            _userManager = userManager;
            _droneRoleValidator = droneRoleValidator;
        }

        public void Initialize()
        {
            if (_validateDatabase.EnsureCreated())
            {
                if (!_droneRoleValidator.ExistRoleAsync(Roles.ROLE_API_DRONE).Result)
                {
                    var resultado = _droneRoleValidator.CreateRoleAsync(Roles.ROLE_API_DRONE).Result;
                    if (!resultado)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.ROLE_API_DRONE}.");
                    }
                }

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "admin_drone",
                        Email = "admin-apiprodutos@teste.com.br",
                        EmailConfirmed = true
                    }, "AdminAPIDrone01!", Roles.ROLE_API_DRONE);

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "usuario_drone",
                        Email = "usrinvalido-apiprodutos@teste.com.br",
                        EmailConfirmed = true
                    }, "UsrInvAPIDrone01!");
            }
        }
        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}
