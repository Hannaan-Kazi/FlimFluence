using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class AddMovieDTO
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public string Summary { get; set; }

        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Decimal Ratings { get; set; }
        public IFormFile? Image { get; set; }


    }
}
