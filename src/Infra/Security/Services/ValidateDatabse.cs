using devboost.dronedelivery.security.domain.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace devboost.dronedelivery.felipe.Security
{
    [ExcludeFromCodeCoverage]
    public class ValidateDatabse : IValidateDatabase
    {
        public ValidateDatabse()
        {
        }
        public bool EnsureCreated()
        {
            return true;
        }
    }
}
