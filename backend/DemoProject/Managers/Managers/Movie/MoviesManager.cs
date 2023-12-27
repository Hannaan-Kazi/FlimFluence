using DataService;
using DataService.Abstract;
using DataService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Managers
{
    public class MoviesManager
    {
        private readonly IMovieDataService _movieDataService;
        public MoviesManager(IMovieDataService movie)
        {
            _movieDataService = movie;
        }

        public async Task<List<MovieDTO>> GetAll()
        {
            return MapMovieDto(await _movieDataService.GetMovies());
        }

        public async Task<MovieDTO?> GetByName(string name)
        {
            return MapMovieDto(await _movieDataService.GetMovieByName(name));
        }


        
        public async Task<ActionResult<bool>> AddMovie(int uId, AddMovieDTO movie)
        {
            int id = uId;
            //if (movie.Image == null) return null;
            //return null;
            //byte[] Image = Encoding.UTF8.GetBytes(movie.Image);
            byte[] imgData;
            using (var memorystream = new MemoryStream())
            {
                movie.Image.CopyTo(memorystream);
                imgData = memorystream.ToArray();
            }
            if (imgData != null)
            {
                var NewMovie = new Movie();
                NewMovie.Title = movie.Title;
                NewMovie.PosterUrl = movie.PosterUrl;
                NewMovie.Summary = movie.Summary;
                NewMovie.ReleaseDate = movie.ReleaseDate;
                NewMovie.Genre = movie.Genre;
                NewMovie.Image = imgData;
                NewMovie.CreatedOn = DateTime.Now;
                NewMovie.CreatedBy = id;
                NewMovie.Ratings = 0;
                return await _movieDataService.SaveMovie(NewMovie);
            }
            else
            {
                return false;
            }
        }

        public async Task<MovieDTO?> GetById(int id)
        {
            var found= await _movieDataService.GetMovieById(id) ;
            if(found == null) { return null; }
            return MapMovieDto(found);
        }

        public async Task<bool> updateMovie(int adminId, int id, AddMovieDTO movie)
        {
            byte[] imgData;
            using (var memorystream = new MemoryStream())
            {
                movie.Image?.CopyTo(memorystream);
                imgData = memorystream.ToArray();
            }
            var found = await _movieDataService.GetMovieById(id);
            if (found == null) { return false; }
            found.Title = movie.Title;
            found.UpdatedOn = DateTime.Now;
            found.Summary = movie.Summary;
            found.ReleaseDate = movie.ReleaseDate;
            found.Genre = movie.Genre;
            found.PosterUrl= movie.PosterUrl;
            found.UpdatedBy = adminId;
            found.Image = imgData;
            return await _movieDataService.UpdateMovie();
        }

        public async Task<bool> DeleteMovie(int admId, int id)
        {
            var found = await _movieDataService.GetMovieById(id);
            if (found == null) { return false; }
            found.DeletedOn= DateTime.Now;
            found.DeletedBy = admId;
            return await _movieDataService.DelMovie();
        }

        public List<MovieDTO> MapMovieDto(List<Movie> movies)
        {
            List<MovieDTO> movieDTOs = new List<MovieDTO>();

            foreach (Movie movie in movies)
            {
                var NewMovie = new MovieDTO();
                NewMovie.MovieId = movie.MovieId;
                NewMovie.Title = movie.Title;
                NewMovie.PosterUrl = movie.PosterUrl;
                NewMovie.Summary = movie.Summary;
                NewMovie.ReleaseDate = (DateTime)movie.ReleaseDate;
                NewMovie.Genre = movie.Genre;
                NewMovie.Ratings = (decimal)movie.Ratings == null ? 0 : (decimal)movie.Ratings;
                if (movie.Image != null)
                {
                    NewMovie.Image = movie.Image;
                }

                movieDTOs.Add(NewMovie);
            }
            return movieDTOs;

        }

        public MovieDTO MapMovieDto(Movie movie)
        {
            var x = new MovieDTO {
            MovieId = movie.MovieId,
            Title = movie.Title,
            PosterUrl = movie.PosterUrl,
            Summary = movie.Summary,
            ReleaseDate = (DateTime)movie.ReleaseDate,
            Genre = movie.Genre,
            Ratings = (decimal)movie.Ratings==null? 0 : (decimal)movie.Ratings
        };
            if (movie.Image != null)
            {
                x.Image = movie.Image;
            }else if (movie.Image== null) {
                x.Image = null;
            }
            return x;

        }




    }
}
