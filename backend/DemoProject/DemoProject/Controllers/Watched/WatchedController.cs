using DataService;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WatchedController : ControllerBase
    {
        private readonly WatchedsManager _watcheds;

        public WatchedController(WatchedsManager watcheds)
        {
            _watcheds = watcheds;
        }
        // GET: api/<WatchedController>
        [HttpGet]
        public async Task<List<WatchedDto>> Get()
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _watcheds.getWatcheds(int.Parse(userId));
            return res;
        }

        [HttpPost]
        public async Task<ActionResult> AddWl([FromBody] int mId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _watcheds.AddWatched(int.Parse(userId), mId) ? Ok(new { status = "Added" }) : Ok(new { status = "Already exists" });

            return Ok(new { status = "Added" });

        }

        [HttpDelete]
        public async Task<bool> RemoveWL([FromBody] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _watcheds.DeleteWatched(int.Parse(userId), id);

        }
        //// GET api/<WatchedController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<WatchedController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<WatchedController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<WatchedController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
