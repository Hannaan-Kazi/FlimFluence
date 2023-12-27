using DataService;
using Managers.Managers;
using Managers.Managers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesManager _movies;

        public MoviesController(MoviesManager movieMan)
        {
            _movies = movieMan;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> getAllMovies()
        {
            return await _movies.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("name")]
        public async Task<ActionResult<MovieDTO?>> GetByName(string name)
        {
            return await _movies.GetByName(name);
        }

        //[HttpPost]
        //public async Task<ActionResult<bool>> AddMovie(Movie movie)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null) { return Forbid(); }
        //    return Ok(await _movies.AddMovie(int.Parse(userId), movie));
        //}

        [AllowAnonymous]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<bool>> AddMovie([FromForm]AddMovieDTO movie)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { return Forbid(); }
            var movies = await _movies.AddMovie(int.Parse(userId), movie);
            //return null;
            return Ok(movies);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<MovieDTO> GetViaId(int id)
        {
            return await _movies.GetById(id);            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMovie([FromForm] AddMovieDTO movie)
        {
            var i = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (i == null) { return Forbid(); };
            return await _movies.updateMovie(int.Parse(i), movie.MovieId, movie)?Ok(new {status="Updated"}):BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var i = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (i == null) { return Forbid(); };
            return await _movies.DeleteMovie(int.Parse(i), id) ? Ok(new { status = "Deleted" }) : BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("test")]
        [Consumes("multipart/form-data")]
        public ActionResult<IFormFile?> UploadFile([FromForm]MovieDTO test)
        {
            if (test.Image == null)
            {
                return BadRequest();
            }

            // Your file processing logic here

            return Ok(test.Image);
        }

    }
}
