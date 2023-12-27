using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class TestDTO
    {
        public string Id { get; set; }
        public IFormFile? ImgData { get; set; }
    }
}
