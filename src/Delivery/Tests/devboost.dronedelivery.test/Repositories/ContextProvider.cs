using devboost.dronedelivery.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace devboost.dronedelivery.test.Repositories
{
    public static class ContextProvider<T> where T : class
    {
        public static DataContext GetContext(List<T> data)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: $"Database-{DateTime.UtcNow.Ticks}")
           .Options;

            // Insert seed data into the database using one instance of the context
            var context = new DataContext(options);
            try
            {
                if (data?.Count > 0)
                {
                    context.AddRange(data);
                    context.SaveChanges();
                }
            }
            catch
            {

            }
            return context;

        }
    }
}
