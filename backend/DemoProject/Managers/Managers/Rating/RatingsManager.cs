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
    public class RatingsManager
    {
        private readonly IRatingDataService _ratingDataService;

        public RatingsManager(IRatingDataService ratingDataService)
        {
            _ratingDataService = ratingDataService;
        }

        public async Task<List<RatingsDto>> getRating(int id)
        {
            return MapRatingDto(await _ratingDataService.getRatings(id));
        }

        public async Task<List<RatingsDto>> getAMovie(int id)
        {
            return MapRatingDto(await _ratingDataService.getAMovieRatings(id));
        }

        public async Task<bool> rate(int id, Rating rating)
        {
            var r = new Rating
            {
                UserId = id,
                MovieId = rating.MovieId,
                Rating1 = rating.Rating1,
                Comment = rating.Comment,
                CreatedOn = DateTime.Now,
                CreatedBy = id
            };
            return await _ratingDataService.giveRating(r);
        }
        public async Task<bool> updateRate(int id, Rating rating)
        {
            var r = new Rating
            {
                UserId = id,
                MovieId = rating.MovieId,
                Rating1 = rating.Rating1,
                Comment = rating.Comment,
                UpdatedOn = DateTime.Now,
                UpdatedBy = id
            };
            return await _ratingDataService.updateRating(r);
        }

        public async Task<bool> delRate(int id, int movId)
        {
            return await _ratingDataService.deleteRating(id, movId);
        }

        public List<RatingsDto > MapRatingDto(List<Rating> ratings)
        {
            List<RatingsDto> list = new List<RatingsDto>();
            
            foreach(Rating rating in ratings)
            {
                list.Add(
                     new RatingsDto
                     {
                         MovieId = (int)rating.MovieId,
                         Rating = rating.Rating1,
                         Comment = rating.Comment
                     });
            }
            return list;
        }

    }
}
