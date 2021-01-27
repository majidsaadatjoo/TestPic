using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestPic.Extensions;
using TestPic.Models;
using TestPic.Services.Classes;

namespace TestPic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  ImageController : ControllerBase
    {
        private readonly IImageUploadServices _imageUpload;

        public ImageController(IImageUploadServices imageUpload)
        {
            this._imageUpload = imageUpload;
        }

        [HttpGet("UploadFromLink")]
        public async Task<IActionResult> FromLink([FromForm]ImageLinkViewModel model)
        {
            string FilePath;
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(model.Url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        
                        byte[] a = await result.Content.ReadAsByteArrayAsync();
                        if (a.Length > (model.Size * 1024))
                            throw new FileLoadException($"حجم تصویر وارد شده نباید بیش از {model.Size} کیلوبایت باشد");
                    }

                }
            }
            using (WebClient client = new WebClient())
            {
                var FileName = $"{Guid.NewGuid()}.jpg";
                 FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", FileName);
                client.DownloadFile(new Uri(model.Url), FilePath);
            }
            return Ok(FilePath);
        } 
        [HttpGet("UploadFormFile")]
        public async Task<IActionResult> FormFile([FromForm]ImageFileViewModel model)
        {
            if (model.Image.Length > (model.Size * 1024))
                throw new FileLoadException($"حجم تصویر وارد شده نباید بیش از {model.Size} کیلوبایت باشد");
            return Ok(await _imageUpload.SaveImageFile(model));
        }  
        [HttpGet("UploadFormBase64")]
        public async Task<IActionResult> FormBase64([FromForm]Image64ViewModel model)
        {
            var ab = model.Image.Split(',')[0];
            byte[] bytes = Convert.FromBase64String(model.Image.Split(',')[1]);
            if (bytes.Length> (model.Size * 1024))
                throw new FileLoadException($"حجم تصویر وارد شده نباید بیش از {model.Size} کیلوبایت باشد");

            return Ok(await _imageUpload.SaveImage64(bytes));
        }
    }
}