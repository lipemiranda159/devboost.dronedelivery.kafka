using System;
using System.ComponentModel.DataAnnotations;

namespace devboost.dronedelivery.core.domain.Entities
{
    public class Drone : IEquatable<Drone>
    {
        [Key]
        public int Id { get; set; }

        public int Capacidade { get; set; }

        public int Velocidade { get; set; }

        public int Autonomia { get; set; }

        public int Carga { get; set; }

        public float Perfomance { get; set; }

        public bool Equals(Drone other)
        {
            return Id == other.Id
            && Capacidade == other.Capacidade
            && Velocidade == other.Velocidade
            && Autonomia == other.Autonomia
            && Carga == other.Carga
            && Perfomance == other.Perfomance;
        }

        public Drone()
        {
        }

        public Drone(int autonomia, int velocidade)
        {
            Velocidade = velocidade;
            Autonomia = autonomia;

        }

        public Drone(int capacidade, int velocidade, int autonomia, int carga)
        {
            Capacidade = capacidade;
            Velocidade = velocidade;
            Autonomia = autonomia;
            Carga = carga;
        }

        public void SetPerformance()
        {
            Perfomance = (Autonomia / 60.0f) * Velocidade;
        }
    }
}
