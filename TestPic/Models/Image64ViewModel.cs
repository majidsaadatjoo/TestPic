using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestPic.Services.Validators;

namespace TestPic.Models
{
    public class Image64ViewModel
    {
        [ImageBase64]
        [Required]
        public string Image { get; set; }
        public int Size { get; set; } // kb?
    }
}
