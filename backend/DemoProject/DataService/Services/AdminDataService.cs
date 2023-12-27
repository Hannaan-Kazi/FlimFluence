using DataService.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class AdminDataService: IAdminDataService
    {
        private readonly ProjectDbContext _dbContext;

        public AdminDataService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Admin>> GetAdmins()
        {
           return await _dbContext.Admins.ToListAsync();
        }

        public async Task<Admin> GetById(int id)
        {
            return await _dbContext.Admins.FindAsync(id);
        }

        public async Task<bool> AddAdmin(Admin admin)
        {
             _dbContext.Admins.Add(admin);
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<Admin> GetByEmail(string email)
        {
            var found = await _dbContext.Admins.FirstOrDefaultAsync(u => u.Email == email);
            return found;
        }

        public async Task<bool> UpdateAdmin(int id, Admin admin)
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AdminSignIn(string email, string password)
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAdmin()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
