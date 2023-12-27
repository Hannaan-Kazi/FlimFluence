using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface IWatchLaterDataService
    {
        public Task<List<WatchLater>> getWL(int id);

        public Task<bool> deleteWL(int UserId, int id);

        public Task<bool> addWl(WatchLater watchLater);
    }
}
