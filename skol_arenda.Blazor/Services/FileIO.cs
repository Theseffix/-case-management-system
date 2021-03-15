using Microsoft.AspNetCore.Hosting;

namespace ITHSManagement.Services
{
    public class FileIO
    {
        IWebHostEnvironment env;
        public FileIO(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void ReadFileIntoDatabase()
        {
            //File path.
            var path = $"{ env.WebRootPath }\\db.xlsx";
        }
    }
}