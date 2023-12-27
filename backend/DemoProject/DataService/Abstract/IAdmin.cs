using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface IAdminDataService
    {
        Task<List<Admin>> GetAdmins();

        Task<Admin> GetById(int id);

        Task<Admin> GetByEmail(string email);

        Task<bool> AddAdmin(Admin admin);

        Task<bool> UpdateAdmin(int id,Admin admin);

        Task<bool> DeleteAdmin();

        Task<bool> AdminSignIn(string email, string password);
    }
}
