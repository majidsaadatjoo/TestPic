using System.Threading.Tasks;
using TestPic.Models;

namespace TestPic.Services.Classes
{
    public interface IImageUploadServices
    {
        Task<string> SaveImage64(byte[] Array);
        Task<string> SaveImageFile(ImageFileViewModel model);
    }
}