using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.ArchivesManager
{
    public class FileManager : IFileManager
    {
        private string imagePath;
        public FileManager(IConfiguration config)
        {
            imagePath = config["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var save_path = Path.Combine(imagePath);
                if (!Directory.Exists(save_path))
                {
                    Directory.CreateDirectory(save_path);
                }

                //internet explorer error 
                //var filename = image.FileName;

                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var imageName = $"img_{Guid.NewGuid().ToString()}{mime}";

                using (var filestream = new FileStream(Path.Combine(save_path, imageName), FileMode.Create))
                {
                    await image.CopyToAsync(filestream);
                }
                return imageName;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }
    }
}
