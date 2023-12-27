using DataService;
using DataService.Abstract;
using DataService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class WLManager
    {
        private readonly IWatchLaterDataService _wLDataService;

        public WLManager(IWatchLaterDataService wLDataService)
        {
            _wLDataService = wLDataService;
        }

        public async Task<List<WatchLater>> GetWatchLaters(int id)
        {
            return await _wLDataService.getWL(id);
        }

        public async Task<bool> DeleteWl(int uid, int id)
        {
            return await _wLDataService.deleteWL(uid, id);
        }

        public async Task<bool> AddWL(int uid, int id)
        {
            var x = new WatchLater
            {
                UserId = uid,
                MovieId = id,
                CreatedOn = DateTime.Now,

            };
            return await _wLDataService.addWl(x);
        }
    }
}
