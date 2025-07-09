using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using on_the_sky.core.entities;

namespace on_the_sky.core.services
{
    public interface iUsersService
    {
        public Task<List<Users>> Getlist();
        public  Task<Users> GetByName(string name, string password);
        public Task ADD(Users u);
        public Task<Users> Put(int id, Users value);
        public Task<Users> Delete(int id);
        public  Task<bool> IsExist(string name, string password);
    }
}
