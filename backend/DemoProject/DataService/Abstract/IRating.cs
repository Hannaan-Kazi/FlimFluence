using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface IRatingDataService
    {
        public Task<List<Rating>> getRatings(int id);

        public Task<List<Rating>> getAMovieRatings(int id);

        public Task<bool> giveRating(Rating r);

        public Task<bool> updateRating(Rating r);

        public Task<bool> deleteRating(int id, int movId);

    }
}
