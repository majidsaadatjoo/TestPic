using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestPic.Extensions
{
    public static class IFormFileExtension
    {
        public static byte[] ToByte(this IFormFile img)
        {
            byte[] image = null;
            using (var fileStream = img.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    image = ms.ToArray();
                }
            }

            return image;
        }
    }
}
