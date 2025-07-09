using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.core.repositories
{
    public interface IFlightRepository
    {
        public Task<List<Flight>> GetAll();
        public Task<Flight> GetById(int id);
        public Task ADD_flight(Flight f);
        public Task<Flight> Put(int id, Flight value);
        public Task<Flight> Delete(int id);
    }
}
