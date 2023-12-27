using DataService.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class WatchedDataService :IWatchedDataservice
    {
        private readonly ProjectDbContext _dbContext;

        public WatchedDataService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Watched>> getWatched(int id)
        {
            return await _dbContext.Watcheds.Where(u=>u.UserId==id).ToListAsync();   
        }

        public async Task<bool> addWatched(Watched x)
        {
            //checking if already exists
            var fal = (_dbContext.Watcheds?.Any(a => a.UserId == x.UserId && a.MovieId == x.MovieId)).GetValueOrDefault();

            if (fal) return false;
            _dbContext.Watcheds.Add(x);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> deleteWatched(int uid, int id)
        {
            //_dbContext.Watched.Remove(_dbContext.Watched.Single(a => a.WatchedId == id && a.UserId == uid));
            _dbContext.Watcheds.Remove(_dbContext.Watcheds.FirstOrDefault(a => a.MovieId == id && a.UserId == uid));
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
