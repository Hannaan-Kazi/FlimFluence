using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface IUserDataService
    {
        Task<List<User>> GetUsers();

        Task<User> GetUser(int id);

        Task<User> GetUserByEmail(string email);

        Task<List<User>> GetCurrentUsers();

        Task<bool> AddUser(User newUser);

        Task<bool> UpdateUser(int id,User newUser);

        Task<bool> DeleteUser(int id);


        Task<bool> SignIn(string email, string password);

    }
}
