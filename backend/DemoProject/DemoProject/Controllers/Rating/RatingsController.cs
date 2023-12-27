using DataService;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RatingsController : ControllerBase
    {
        // GET: api/<RatingsController>
        private readonly RatingsManager _ratings;

        public RatingsController(RatingsManager ratingsManager)
        {
            _ratings = ratingsManager;
        }

        [HttpGet]
        public async Task<List<RatingsDto>> Get()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //return await _ratings.getRating(3);


            return await _ratings.getRating(int.Parse(userid));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<List<RatingsDto>> getByMovieId(int id)
        {
            return await _ratings.getAMovie(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Rating rating)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _ratings.rate(int.Parse(userid), rating));

        }

        [HttpPut]
        public async Task<ActionResult> Put(Rating rating)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _ratings.updateRate(int.Parse(userid), rating));

        }

        [HttpDelete]
        public async Task<bool> delete([FromBody] int movId)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _ratings.delRate(int.Parse(userid),movId );
        }


        // GET api/<RatingsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<RatingsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RatingsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RatingsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
