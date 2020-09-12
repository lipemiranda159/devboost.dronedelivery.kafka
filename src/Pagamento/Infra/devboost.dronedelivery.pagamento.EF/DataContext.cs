using devboost.dronedelivery.core.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace devboost.dronedelivery.pagamento.EF
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Pagamento> Pagamento { get; set; }

    }

}
