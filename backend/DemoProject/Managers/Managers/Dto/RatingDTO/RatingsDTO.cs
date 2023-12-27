using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class RatingsDto
    {
        public int MovieId { get; set; }

        public string Comment { get; set; }

        public decimal? Rating { get; set; }
    }
}
