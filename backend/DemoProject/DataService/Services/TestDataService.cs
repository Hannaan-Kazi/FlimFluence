using DataService.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services
{
    public class TestDataService : ITestDataService
    {     

    private readonly ProjectDbContext _dbContext;
        
        public TestDataService(ProjectDbContext dbContext) {  _dbContext = dbContext; }

        public async Task<List<ImageTable>> imageGet()
        {

            return await _dbContext.ImageTables.ToListAsync();
        }

        public async Task<bool> imagePost(ImageTable image)
        {
            _dbContext.ImageTables.Add(image);
            return await _dbContext.SaveChangesAsync()>0;

            //if (a ?) return true;
            //return Ok(new { ImageId = image.Id });
        }

        
    }
}
