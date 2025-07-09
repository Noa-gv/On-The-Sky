using Microsoft.EntityFrameworkCore;
using on_the_sky.core.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.Data.repositories
{
    public class PlaceRepositories:IplaceRepository
    {
        private readonly Datacontext _context;
        public PlaceRepositories(Datacontext context)
        {
            _context = context;
        }
        public async Task<List<Places>> GetAll()
        {
            return await _context.PlacesDB.Include(p=>p.flight_list).ToListAsync();
        }
        public async Task<Places> GetById(int id)
        {
            return await _context.PlacesDB.FirstOrDefaultAsync(x => x.countryid == id);
        }
        public async Task ADD(Places place)
        {
             _context.PlacesDB.Add(place);
             await _context.SaveChangesAsync ();

        }

        public async Task<Places> Put(int id, Places value)
        {
            var place_ = await _context.PlacesDB.FirstOrDefaultAsync(p => p.countryid == id);
            place_.country = value.country;
            place_.flight_list = value.flight_list;
            await _context.SaveChangesAsync();

            return place_;

        }
        public async  Task<Places>  Delete(int id)
        { 
            Places tmp = null;
            var place_ = await _context.PlacesDB.FirstOrDefaultAsync(e => e.countryid == id);
            if (place_ != null)
            {
                tmp = place_;
                _context.PlacesDB.Remove(place_);
            } 
            await  _context.SaveChangesAsync();
            return tmp;
        }
    }
}
