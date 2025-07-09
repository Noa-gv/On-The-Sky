using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using on_the_sky.core.entities;
using on_the_sky.core.repositories;
using on_the_sky.core.services;

namespace on_the_sky.service
{
    public class UsersServicecs: iUsersService
    {
        private iUsersRipository _Usersrepositories;
        public UsersServicecs(iUsersRipository Usersrepositories)
        {
            _Usersrepositories = Usersrepositories;
        }
        public async Task<List<Users>> Getlist()
        {
            return await _Usersrepositories.GetAll();

        }
        public async Task<Users> GetByName(string name, string password)
        {
            return await _Usersrepositories.GetByName(name,password);
        }
        public async Task ADD(Users u)
        {
            await _Usersrepositories.ADD_user(u);
        }
        public async Task<Users> Put(int id, Users value)
        {
            return await _Usersrepositories.Put(id, value);

        }
        public async Task<Users> Delete(int id)
        {
            return await _Usersrepositories.Delete(id);
        }
        public async Task<bool> IsExist(string name, string password)
        {
            return await _Usersrepositories.IsExist(name, password);
        }
    }
}

