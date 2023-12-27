using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface IWatchedDataservice
    {
        public Task<List<Watched>> getWatched(int id);

        public Task<bool> deleteWatched(int UserId, int id);

        public Task<bool> addWatched(Watched watched);
    }
}
