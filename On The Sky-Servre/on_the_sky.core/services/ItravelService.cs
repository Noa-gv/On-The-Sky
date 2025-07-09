using on_the_sky.core.entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace on_the_sky.core.services
{
    public interface ItravelService
    {
        Task<List<Travel>> GetAll();
        Task<Travel> GetById(int id);
        Task<List<Travel>> GetByUserId(int userId);  // פונקציה חדשה
        Task ADD(Travel travel);
        Task<Travel> Put(int id, Travel value);
        Task<Travel> Delete(int id);
    }
}
