using DataService.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class WLDataService: IWatchLaterDataService
    {
        private readonly ProjectDbContext _dbContext;
        public WLDataService(ProjectDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<WatchLater>> getWL(int id)
        {
            return await _dbContext.WatchLaters.Where(w=>w.UserId == id).ToListAsync();

        }

        public async Task<bool> addWl(WatchLater x)
        {
            //checking if already exists
            var fal= (_dbContext.WatchLaters?.Any(a => a.UserId == x.UserId && a.MovieId == x.MovieId)).GetValueOrDefault();

            if (fal) return false;
            _dbContext.WatchLaters.Add(x);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> deleteWL(int uid, int id)
        {
            _dbContext.WatchLaters.Remove(_dbContext.WatchLaters.Single(a => a.MovieId == id && a.UserId==uid));
            return await _dbContext.SaveChangesAsync()>0;
        }



        
    }
}
