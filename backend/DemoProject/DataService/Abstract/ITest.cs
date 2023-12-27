using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Abstract
{
    public interface ITestDataService
    {
        Task<List<ImageTable>> imageGet();

        Task<bool> imagePost(ImageTable imageTable);

    }
}
