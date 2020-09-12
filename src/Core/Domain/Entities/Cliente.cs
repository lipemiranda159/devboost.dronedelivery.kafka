using System;
using System.ComponentModel.DataAnnotations;

namespace devboost.dronedelivery.core.domain.Entities
{
    public class Cliente : IEquatable<Cliente>
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Cliente other)
        {
            return Id.Equals(other.Id) &&
                Nome.Equals(other.Nome) &&
                Latitude.Equals(other.Latitude) &&
                Longitude.Equals(other.Longitude) &&
                UserId.Equals(other.UserId) &&
                Password.Equals(other.Password);
        }
    }
}
