using Microsoft.AspNetCore.Hosting;

namespace Store.Extensions
{
    public static class FileUpload
    {
        public static async Task<string> Upload(IFormFile file)
        {


            if (file.Length > 0)
            {
                // Generate a unique file name (optional)
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                string dirctoryPath = Path.GetFullPath("wwwroot\\ProductsImages");

                // Define the file path where you want to save the uploaded file
                var filePath = Path.Combine(dirctoryPath, fileName);

                // Save the file to the specified path
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            return null;

        }

        public static async Task Delete(string fileName)
        {
            string dirctoryPath = Path.GetFullPath("wwwroot\\ProductsImages");
            var filePath = Path.Combine(dirctoryPath, fileName);
            File.Delete(filePath);
        }

    }
}

