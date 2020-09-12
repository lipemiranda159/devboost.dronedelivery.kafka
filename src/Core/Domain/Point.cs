namespace devboost.dronedelivery.core.domain
{
    public class Point
    {
        private const double LATITUDE_BASE = -23.5880684;
        private const double LONGITUDE_BASE = -46.6564195;

        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

        }

        public Point()
        {
            Latitude = LATITUDE_BASE;
            Longitude = LONGITUDE_BASE;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
