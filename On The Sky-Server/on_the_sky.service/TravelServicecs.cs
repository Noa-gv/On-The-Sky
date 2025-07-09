using on_the_sky.core.entities;
using on_the_sky.core.repositories;
using on_the_sky.core.services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace on_the_sky.service
{
    public class TravelService : ItravelService
    {
        private readonly ITravelRepository _travelRepository;

        public TravelService(ITravelRepository travelRepository)
        {
            _travelRepository = travelRepository;
        }

        public async Task<List<Travel>> GetAll()
        {
            return await _travelRepository.Getlist();
        }

        public async Task<Travel> GetById(int id)
        {
            return await _travelRepository.GetById(id);
        }

        public async Task<List<Travel>> GetByUserId(int userId)  // מימוש הפונקציה החדשה
        {
            return await _travelRepository.GetByUserId(userId);
        }

        public async Task ADD(Travel travel)
        {
            await _travelRepository.ADD(travel);
        }

        public async Task<Travel> Put(int id, Travel value)
        {
            return await _travelRepository.Put(id, value);
        }

        public async Task<Travel> Delete(int id)
        {
            return await _travelRepository.Delete(id);
        }
    }
}
