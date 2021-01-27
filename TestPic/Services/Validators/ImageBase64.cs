using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace TestPic.Services.Validators
{
    public class ImageBase64Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Pic = value as string;
            if (string.IsNullOrEmpty(Pic))
                return new ValidationResult("تصویر را مشخص کنید!");
            try
            {
                var buffer = Convert.FromBase64String(Pic.Split(",")[1]);
                MemoryStream stream = new MemoryStream(buffer);
                Image.FromStream(stream);
            }
            catch
            {
                return new ValidationResult("تصویر ارسالی مورد تایید نیست!");
            }
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
            var Format = Pic.Split(",")[0];
            if (IMageContentType.FirstOrDefault(p => p.Contains(Format.Split(':')[1].Split(';')[0])) == null)
                return new ValidationResult("فرمت وارد شده معتبر نیست!");
            return ValidationResult.Success;
        }
    }
}
