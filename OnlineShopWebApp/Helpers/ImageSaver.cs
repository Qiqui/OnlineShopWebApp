using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers
{
    public static class ImageSaver
    {
        public static List<string> UpdateProductImage(IWebHostEnvironment webHostEnvironment, ProductUpdateViewModel productVM, List<string> imagePaths)
        {
            imagePaths ??= new List<string>();
            var productImagesPath = Path.Combine(webHostEnvironment.WebRootPath + "/images/products/");
            if (!Directory.Exists(productImagesPath))
                Directory.CreateDirectory(productImagesPath);

            foreach (var uploadedFile in productVM.UploadedFiles)
            {
                var fileName = $"{Guid.NewGuid()}.{uploadedFile.FileName.Split(".").Last()}";
                using (var fileSteam = new FileStream(productImagesPath + fileName, FileMode.Create))
                    uploadedFile.CopyTo(fileSteam);

                imagePaths.Add($"/images/products/{fileName}");
            }

            return imagePaths;
        }

        public static List<string> SaveProductImage(IWebHostEnvironment webHostEnvironment, ProductAddViewModel productVM)
        {
            var imagePaths = new List<string>();
            var productImagesPath = Path.Combine(webHostEnvironment.WebRootPath + "/images/products/");
            if (!Directory.Exists(productImagesPath))
                Directory.CreateDirectory(productImagesPath);

            foreach (var uploadedFile in productVM.UploadedFiles)
            {
                var fileName = $"{Guid.NewGuid()}.{uploadedFile.FileName.Split(".").Last()}";
                using (var fileSteam = new FileStream(productImagesPath + fileName, FileMode.Create))
                    uploadedFile.CopyTo(fileSteam);
                imagePaths.Add($"/images/products/{fileName}");
            }

            return imagePaths;
        }

        public static string UpdateUserImage(IWebHostEnvironment webHostEnvironmentm, IFormFile uploadedFile)
        {
            var userImagePath = Path.Combine(webHostEnvironmentm.WebRootPath + "/images/usersImages/");
            if(!Directory.Exists(userImagePath))
                Directory.CreateDirectory(userImagePath);

            var fileName = $"{Guid.NewGuid()}.{uploadedFile.FileName.Split(".").Last()}";
            using (var fileStream = new FileStream(userImagePath + fileName, FileMode.Create))
                uploadedFile.CopyTo(fileStream);

            return $"/images/usersImages/{fileName}";
        }
    }
}
