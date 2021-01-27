using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestPic.Services.Validators
{
    public class ImageFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Value Is Null!!");

            var IMageContentType = new List<string>
            {
                "image/jpg",
                "image/jpeg",
                "image/png",
            } as IReadOnlyCollection<string>;

            var ImageExtension = new List<string>
            {
                ".jpg",
                ".jpeg",
                ".png",
            } as IReadOnlyCollection<string>;

            var Image = value as IFormFile;

            var ContentType = Image.ContentType;
            var Extension = Path.GetExtension(Image.FileName);

            if (!IMageContentType.Contains(ContentType) | !ImageExtension.Contains(Extension))
                return new ValidationResult("فرمت تصویر وارد شده صحیح نیست!");     
            return ValidationResult.Success;
        }
    }
}
