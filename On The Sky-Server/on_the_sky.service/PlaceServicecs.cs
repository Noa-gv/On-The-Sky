using on_the_sky.core.repositories;
using on_the_sky.core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.service
{
    public class PlaceServicecs : IplaceService
    {
        private IplaceRepository _placeRepositories;
        public PlaceServicecs(IplaceRepository placeRepositories)
        {
            _placeRepositories = placeRepositories;
        }
        public async Task<List<Places>> GetAll()
        {

            return await _placeRepositories.GetAll();
        }
        public async Task<Places> GetById(int id)
        {
            return await _placeRepositories.GetById(id);
        }
        public async Task ADD(Places place)
        {
           await _placeRepositories.ADD(place);
        }
        public async Task<Places>  Put(int id, Places value)
        {
           return await _placeRepositories.Put(id, value);
        }
        public async Task<Places> Delete(int id)
        {
            return await _placeRepositories.Delete(id);
        }
    }
}
