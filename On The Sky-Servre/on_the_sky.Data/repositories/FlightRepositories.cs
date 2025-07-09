using Microsoft.EntityFrameworkCore;
using on_the_sky.core.repositories;
using on_the_sky.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.Data.repositories
{
    public class FlightRepositories : IFlightRepository
    {
        private readonly Datacontext _context;

        public FlightRepositories(Datacontext context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetAll()
        {
            return await _context.FlightDB.Include(f => f.country).Include(f=>f.Travels).ToListAsync();
        }
        public async Task<Flight>  GetById(int id)/* --*/
        {
            return await _context.FlightDB.FirstOrDefaultAsync(x => x.flightid == id);

        }
        public async Task ADD_flight(Flight f)
        {
            _context.FlightDB.Add(f);
            await _context.SaveChangesAsync();

        }
        public async Task<Flight> Put(int id, Flight value)
        {
            var fly= await _context.FlightDB.FirstOrDefaultAsync(f => f.flightid == id);

            //var index = flight_db.FindIndex(e => e.flightid == id);

            fly.country = value.country;
            fly.countryID = value.countryID;
            fly.flighttime = value.flighttime;
            fly.Price = value.Price;//הוספת סכום כרטיס טיסה
            fly.Maximum = value.Maximum;
            fly.amount = value.amount;
            await _context.SaveChangesAsync();

            return fly;
        }
        public async Task<Flight> Delete(int id)
        {
            Flight tmp = null;
            var fly = await _context.FlightDB.FirstOrDefaultAsync(f => f.flightid == id);
            if (fly != null)
            {
                tmp = fly;
                _context.FlightDB.Remove(fly);
            }
            await _context.SaveChangesAsync();
            return tmp;
        }
    }
}
