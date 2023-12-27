using DataService;
using Managers.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestImageController : ControllerBase
    {
        private readonly TestManager _image;

        public TestImageController(TestManager image)
        {
            _image = image;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImageTable>>> Get()
        {
            return Ok(await _image.imageGet());
        }
      
        

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadImage([FromForm] ImageTable img)
        {
            IFormFile formFile = HttpContext.Request.Form.Files.First();
            var res =await _image.PostImage(formFile);
            return Ok(true);

        }

        [HttpPost("ToAssets")]
        public IActionResult UploadFile(IFormFile file)
        {
            // Check if the file is valid
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");
            // Generate a unique filename or use a specific naming convention
            var newFileName = GenerateUniqueFileName(file.FileName);

            // Specify the path where you want to save the file
            var filePath = Path.Combine("C:/Users/Hannaan Kazi/Desktop/HFK/ProjectFrontEnd/" +
                "project-front-end/src/assets/images", file.FileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok("File uploaded successfully");


             string GenerateUniqueFileName(string originalFileName)
            {
                // Generate a unique filename based on the original filename
                var uniquePart = Guid.NewGuid().ToString().Substring(0, 8);
                var fileExtension = Path.GetExtension(originalFileName);
                var newFileName = $"{uniquePart}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";

                return newFileName;
            }
        }
    }
}
