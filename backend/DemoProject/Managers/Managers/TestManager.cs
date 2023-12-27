using DataService;
using DataService.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Managers.Managers
{
    public class TestManager
    {
        private readonly ITestDataService _imageTableDataService;

        public TestManager(ITestDataService imageTable)
        {
            _imageTableDataService = imageTable;
        }

        public async Task<List<ImageTable>> imageGet()
        {
            var l = new List<ImageTable>();
          var a= await _imageTableDataService.imageGet();
            foreach (var item in a)
            {
                ImageTableToFormFile(item);
                l.Add(item);
            }
            return l;
        }


        public async Task<bool> PostImage(IFormFile file)
        {

            var i = MaptoImageTable(file);
            //var image = new ImageTable { Data = img.Data};
            return await _imageTableDataService.imagePost(i);
        }

        public ImageTable MaptoImageTable (IFormFile formfile)
        {
            using (BinaryReader reader = new BinaryReader(formfile.OpenReadStream()))
            {
                byte[] filedata = reader.ReadBytes((int)formfile.Length);
                var res = new ImageTable()
                {
                    Data = filedata
                };
                return res;
            }
            //byte[] filedata;
            //using (var memorystream = new MemoryStream())
            //{
            //    formfile.CopyTo(memorystream);
            //    filedata = memorystream.ToArray();
            //}
            
        }

        public IFormFile ImageTableToFormFile(ImageTable imageTable)
        {
            // Assuming ImageTable has a property named FileData of type byte[]
            var fileStream = new MemoryStream(imageTable.Data);

            // You may need to replace "FileName" with the actual file name you want to use
            var fileName = "Image.jpg";

            var formFile = new FormFile(fileStream, 0, fileStream.Length, "FileData", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg" // You may need to adjust the content type based on your file type
            };

            return formFile;
        }
    }
}
