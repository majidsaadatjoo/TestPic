using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TestPic.Models
{
    public class ImageLinkViewModel
    {
        [Url]
        [Required]
        public string Url { get; set; }
        public int Size { get; set; } // Kb?
    }
}
