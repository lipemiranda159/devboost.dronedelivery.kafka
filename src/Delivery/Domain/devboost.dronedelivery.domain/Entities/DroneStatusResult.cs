namespace devboost.dronedelivery.domain.Entities
{
    public class DroneStatusResult
    {
        public int Id { get; set; }

        public int Capacidade { get; set; }

        public int Velocidade { get; set; }

        public int Autonomia { get; set; }

        public int Carga { get; set; }

        public float Perfomance { get; set; }

        public int SomaPeso { get; set; }
        public int SomaDistancia { get; set; }
    }

}
