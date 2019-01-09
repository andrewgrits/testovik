using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Web.Misc
{
    public static class FileUploader
    {
        public static async Task<string> UploadImage(IFormFile uploadedFile, IHostingEnvironment appEnvironment)
        {
            if (!Validator.IsImage(uploadedFile.FileName))
            {
                return string.Empty;
            }

            var path = $"/images/tests/{StringGenerator.RandomString(10)}{Path.GetExtension(uploadedFile.FileName)}";
            var fullPath = appEnvironment.WebRootPath + path;

            using (var fileStream = new FileStream(fullPath, mode: FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            return path;
        }
    }
}
