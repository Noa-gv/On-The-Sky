using on_the_sky.core.repositories;
using on_the_sky.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.service
{
    public class FlightServicecs : IFlightService
    {
        private IFlightRepository _Flightrepositories;
        public FlightServicecs(IFlightRepository flightrepositories)
        {
            _Flightrepositories = flightrepositories;
        }
        public async Task<List<Flight>> Getlist()
        {
            return await _Flightrepositories.GetAll();

        }
        public async Task<Flight> GetById(int id) {
            return await _Flightrepositories.GetById(id);
        }
        public async Task ADD(Flight f) {
           await _Flightrepositories.ADD_flight(f);
        }
        public async Task<Flight> Put(int id, Flight value)
        {
          return await _Flightrepositories.Put( id, value);
        
        }
        public async Task<Flight>  Delete(int id)
        {
            return await _Flightrepositories.Delete( id);
        }
    }
}
