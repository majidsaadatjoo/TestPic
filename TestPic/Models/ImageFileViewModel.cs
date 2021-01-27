using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestPic.Services.Validators;

namespace TestPic.Models
{
    public class ImageFileViewModel
    {
        [Required]
        [ImageFile]
        public IFormFile Image { get; set; }
        public int Size { get; set; }
    }
}
