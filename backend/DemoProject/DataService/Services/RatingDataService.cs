using DataService.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataService.Services
{
    public class RatingDataService: IRatingDataService
    {
        private readonly ProjectDbContext _dbContext;

        public RatingDataService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Rating>> getRatings(int id)
        {
            return await _dbContext.Ratings.Where(w => w.UserId == id).ToListAsync();


        }

        public async Task<List<Rating>> getAMovieRatings(int id)
        {
            return await _dbContext.Ratings.Where(w => w.MovieId == id).ToListAsync();
            //return await _dbContext.Ratings.Where(w => w.UserId == id).ToListAsync();

        }

        public async Task<bool> giveRating(Rating r)
        {
            //using (SqlCommand command = new SqlCommand
            //    ("INSERT INTO YourTableName (UserId, MovieId, Rating, Comment, CreatedOn, CreatedBy) VALUES (@UserId, @MovieId, @Rating, @Comment,@CreatedOn, @CreatedBy )"))
            //{
            //    // Add parameters to prevent SQL injection
            //    command.Parameters.AddWithValue("@UserId", r.UserId);
            //    command.Parameters.AddWithValue("@MovieId", r.MovieId);
            //    command.Parameters.AddWithValue("@Rating", r.Rating1);
            //    command.Parameters.AddWithValue("@Comment", r.Comment);
            //    command.Parameters.AddWithValue("@CreatedOn", r.CreatedOn);
            //    command.Parameters.AddWithValue("@CreatedBy", r.CreatedBy);


            //    // Execute the command
            //    int rowsAffected = command.ExecuteNonQuery();

            //    Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");

            //}
    //        _dbContext.Ratings
    //.ExecuteRawSql();
    //        _dbContext.People
    //.FromSqlRaw($"SELECT * FROM People WHERE Age = {ageParameter}")
    //        _dbContext.Ratings.Add(r);
            var p1 = new SqlParameter("@UserId", r.UserId);
            var p2 = new SqlParameter("@MovieId", r.MovieId);
            var p3 = new SqlParameter("@Rating", r.Rating1);
            var p4 = new SqlParameter("@Comment", r.Comment);
            var p5 = new SqlParameter("@CreatedOn", r.CreatedOn);
            var p6 = new SqlParameter("@CreatedBy", r.CreatedBy);



            _dbContext.Database.ExecuteSqlRaw("INSERT INTO Ratings (UserId, MovieId, Rating, Comment, CreatedOn, CreatedBy)" +
    " VALUES (@UserId, @MovieId, @Rating, @Comment,@CreatedOn, @CreatedBy )", p1, p2, p3, p4, p5,p6);
            //return await _dbContext.SaveChangesAsync()>0; isme output clause hai isiliye issue aa raha tha because into clause nhi hai
            return true;
        }

        public async Task<bool> updateRating(Rating r)
        {
            var p1 = new SqlParameter("@UserId", r.UserId);
            var p2 = new SqlParameter("@MovieId", r.MovieId);
            var p3 = new SqlParameter("@Rating", r.Rating1);
            var p4 = new SqlParameter("@Comment", r.Comment);
            var p5 = new SqlParameter("@UpdatedOn", r.UpdatedOn);
            var p6 = new SqlParameter("@UpdatedBy", r.UpdatedBy);
            _dbContext.Database.ExecuteSqlRaw("UPDATE ratings SET Rating = @Rating, Comment=@Comment, UpdatedOn=@UpdatedOn, UpdatedBy=@UpdatedBy" +
                " WHERE UserId = @UserId AND MovieId= @MovieId", p3, p4, p5, p6, p1, p2);
            return true;

        }

        public async Task<bool> deleteRating(int id, int movId)
        {
            var p1 = new SqlParameter("@UserId", id);
            var p2 = new SqlParameter("@MovieId", movId);

            _dbContext.Database.ExecuteSqlRaw("DELETE FROM Ratings WHERE UserId = @UserId and MovieId= @MovieId", p1, p2);
            return true;

        }
    }
}
