using DataService.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class UserDataService: IUserDataService
    {
        private readonly ProjectDbContext _dbContext;

        public UserDataService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            var found = await _dbContext.Users.FindAsync(id);
            return found;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            //var found = _dbContext.Users.AnyAsync(u => u.Email == email);
            var found =await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return found;
        }

        public async Task<List<User>> GetCurrentUsers()
        {
            return await _dbContext.Users.Where(u=>u.DeletedOn == null).ToListAsync();
            //return found;

        }

        public async Task<bool> AddUser(User newUser)
        {
            _dbContext.Users.Add(newUser);
            var inserted =  await _dbContext.SaveChangesAsync();
            return inserted==1?true:false;
        }


        public async Task<bool> UpdateUser(int id,User newUser)
        {
            //_dbContext.Users.Add(newUser);
            return await _dbContext.SaveChangesAsync()>0;

        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

      

        public async Task<bool> SignIn(string email, string password)
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
