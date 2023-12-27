using DataService;
using Managers.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WatchLaterController : ControllerBase
    {
        private readonly WLManager _wLManager;

        public WatchLaterController(WLManager wLManager)
        {
            _wLManager = wLManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<WatchLater>>> GetWL()
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _wLManager.GetWatchLaters(int.Parse(userId)));

        }

        [HttpPost]
        public async Task<ActionResult> AddWl([FromBody]int mId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
            return await _wLManager.AddWL(int.Parse(userId), mId)? Ok(new { status = "Added" }): Ok(new { status = "Already exists" });

            return Ok(new { status = "Added" });

        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveWL(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _wLManager.DeleteWl(int.Parse(userId),id);

        }

        //private bool WatchLaterExists(int id)
        //{
        //    return (_dbContext.WatchLaters?.Any(e => e.WatchLaterId == id)).GetValueOrDefault();
        //}
    }
}
