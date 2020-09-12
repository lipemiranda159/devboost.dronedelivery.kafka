using devboost.dronedelivery.core.domain;

namespace devboost.dronedelivery.domain.Interfaces
{
    public interface ICoordinateService
    {
        double GetKmDistance(Point originPoint, Point destPoint);
    }
}
