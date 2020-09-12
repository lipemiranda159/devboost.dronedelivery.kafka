using devboost.dronedelivery.security.domain.Entites;

namespace devboost.dronedelivery.security.domain.Extensions
{
    public static class LoginDtoExtensions
    {
        public static bool HasClient(this LoginDTO cliente)
        {
            return cliente != null && !string.IsNullOrWhiteSpace(cliente.UserId);
        }
    }
}
