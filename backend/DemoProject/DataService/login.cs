using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class login
    {
        public string Email { get; set; }
        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

    }
}
