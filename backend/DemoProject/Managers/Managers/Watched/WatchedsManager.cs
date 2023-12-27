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
    public class WatchedsManager
    {
        private readonly IWatchedDataservice _watchedDataservice;

        public WatchedsManager(IWatchedDataservice watched)
        {
            _watchedDataservice = watched;
        }

        public async Task<List<WatchedDto>> getWatcheds(int id)
        {
            return MapWatchedDto( await _watchedDataservice.getWatched(id));
        }        

        public async Task<bool> AddWatched(int uid, int id)
        {
            var x = new Watched
            {
                UserId = uid,
                MovieId = id,
                CreatedOn = DateTime.Now,
            };
            return await _watchedDataservice.addWatched(x);
        }

        public async Task<bool> DeleteWatched(int uid, int id)
        {
            return await _watchedDataservice.deleteWatched(uid, id);
        }


        public List<WatchedDto> MapWatchedDto(List<Watched> watcheds)
        {
            List<WatchedDto> list = new List<WatchedDto>();

            foreach (Watched watched in watcheds)
            {
                list.Add(
                     new WatchedDto
                     {
                         MovieId = (int)watched.MovieId,
                         Rating = watched.Rating,
                         Comment = watched.Comment,
                     });
            }
            return list;
        }
    }
}
