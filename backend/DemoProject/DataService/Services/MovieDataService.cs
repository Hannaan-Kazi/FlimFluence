using Microsoft.EntityFrameworkCore;
using DataService.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class MovieDataService: IMovieDataService
    {
        private readonly ProjectDbContext _dbContext ;

        public MovieDataService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext ;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie?> GetMovieByName(string name)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(m=>m.Title==name);
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _dbContext.Movies.FindAsync(id);
        }

        public async Task<bool> SaveMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            return await _dbContext.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateMovie()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DelMovie()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
