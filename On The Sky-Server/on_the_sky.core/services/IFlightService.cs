using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.core.services
{
    public interface IFlightService
    {
      
        public  Task<List<Flight>> Getlist();
        public Task<Flight> GetById(int id);
        public Task ADD(Flight f);
        public Task<Flight> Put(int id, Flight value);
        public Task<Flight> Delete(int id);
    }
}
