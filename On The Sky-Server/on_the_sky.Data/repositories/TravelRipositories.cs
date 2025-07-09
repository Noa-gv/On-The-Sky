using Microsoft.EntityFrameworkCore;
using on_the_sky.core.repositories;
using on_the_sky.core.entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace on_the_sky.Data.repositories
{
    public class TravelRepository : ITravelRepository
    {
        private readonly Datacontext _context;

        public TravelRepository(Datacontext context)
        {
            _context = context;
        }

        public async Task<List<Travel>> Getlist()
        {
            return await _context.TravelsDB
                .Include(t => t.Flight)
                    .ThenInclude(f => f.country) // שם השדה הנכון
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<Travel> GetById(int id)
        {
            return await _context.TravelsDB
                .Include(t => t.Flight)
                    .ThenInclude(f => f.country)
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.TravelId == id);
        }

        public async Task<List<Travel>> GetByUserId(int userId)
        {
            return await _context.TravelsDB
                .Include(t => t.Flight)
                    .ThenInclude(f => f.country)
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task ADD(Travel travel)
        {
            _context.TravelsDB.Add(travel);
            await _context.SaveChangesAsync();
        }

        public async Task<Travel> Put(int id, Travel value)
        {
            var trv = await _context.TravelsDB.FirstOrDefaultAsync(t => t.TravelId == id);
            if (trv != null)
            {
                trv.FlightId = value.FlightId;
                trv.AmountTickets = value.AmountTickets;
                await _context.SaveChangesAsync();
            }

            return trv;
        }


        public async Task<Travel> Delete(int id)
        {
            var trv = await _context.TravelsDB.FirstOrDefaultAsync(e => e.TravelId == id);
            if (trv != null)
            {
                _context.TravelsDB.Remove(trv);
                await _context.SaveChangesAsync();
            }

            return trv;
        }
    }
}
