using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProfilePictureService : IProfilePictureService
    {
        private readonly IProfilesService _profilesService;
        public ProfilePictureService(IProfilesService profilesService) 
        { 
            _profilesService = profilesService;
        }
        public async Task CreateThumbnail(IFormFile file)
        {
            var stream = file.OpenReadStream();
            string root = AppContext.BaseDirectory;
            string path = Path.Combine(root, "ProfilePictures");
            string _path = Path.Combine(path, "Thumbnails");

            if (!Directory.Exists(_path)) 
            {
                Directory.CreateDirectory(_path);
            }

            string imagePath = Path.Combine(_path, file.Name);

            using (Image image = Image.Load(stream))
            {
                int width = image.Width / 2;
                int height = image.Height / 2;
                image.Mutate(x => x.Resize(width, height));

                image.Save(imagePath);
            }
        }

        public async Task<string> UploadProfilePicture(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string filename = Guid.NewGuid().ToString() + extension;

            string root = AppContext.BaseDirectory;
            string path = Path.Combine(root, "ProfilePictures");

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string _path = Path.Combine(path, "FullSized");

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            string filePath = Path.Combine(_path, filename);

            string __path = Path.Combine(path, "Thumbnails");

            if (!Directory.Exists(__path))
            {
                Directory.CreateDirectory(__path);
            }

            string thumbnailPath = Path.Combine(__path, filename);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);

                stream.Position = 0;

                using (Image image = Image.Load(stream))
                {
                    int width = 100;
                    int height = 0;
                    image.Mutate(x => x.Resize(width, height));

                    image.SaveAsPng(thumbnailPath);
                    
                }
            }

            return filename;
        }
    }
}
