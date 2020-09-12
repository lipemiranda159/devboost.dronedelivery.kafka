using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.test.Repositories
{
    public class CommandExecutorTest<T> : ICommandExecutor<DroneStatusResult>
    { 

        public IEnumerable<DroneStatusResult> ExcecuteCommand(string command)
        {
            return SetupTests.GetDroneStatusResults();
        }

        public Task ExecuteCommandAsync(string command)
        {
            throw new NotImplementedException();
        }

    }
}
