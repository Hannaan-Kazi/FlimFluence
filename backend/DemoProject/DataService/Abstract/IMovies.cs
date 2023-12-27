using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface IMovieDataService
    {
        public Task<List<Movie>> GetMovies();

        public Task<Movie> GetMovieById(int id);

        public Task<Movie> GetMovieByName(string name);

        public Task<bool> SaveMovie(Movie movie);

        public Task<bool> UpdateMovie();

        public Task<bool> DelMovie();
    }
}
