using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestPic.Extensions;
using TestPic.Models;

namespace TestPic.Services.Classes
{
    public class ImageUploadServices : IImageUploadServices
    {
        public async Task<string> SaveImageFile(ImageFileViewModel model)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", $"{Guid.NewGuid()}.jpg");
            await File.WriteAllBytesAsync(FilePath, model.Image.ToByte());
            return FilePath;
        }
        public async Task<string> SaveImage64(byte[] Array)
        {
            return await Task.Run(() =>
            {
                Image image;
                using (MemoryStream ms = new MemoryStream(Array))
                {
                    image = Image.FromStream(ms);
                }
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", $"{Guid.NewGuid()}.jpg");

                image.Save(FilePath);
                return FilePath;

            });
          
              
            
        }
    }
}
