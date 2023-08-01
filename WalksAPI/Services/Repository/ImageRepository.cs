using WalksAPI.Database.Context;
using WalksAPI.Database.DomainModel;
using WalksAPI.Services.IRepository;

namespace WalksAPI.Services.Repository
{
    public class ImageRepository : IImageRepository
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public WalksDataBaseContext WalksDataBaseContext { get; }

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            WalksDataBaseContext walksDataBaseContext)
        {
            WebHostEnvironment = webHostEnvironment;
            HttpContextAccessor = httpContextAccessor;
            WalksDataBaseContext = walksDataBaseContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(WebHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            // Local Upload
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // Web Upload
            var urlFilePath = $"{HttpContextAccessor.HttpContext.Request.Scheme}://{HttpContextAccessor.HttpContext.Request.Host}{HttpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            
            image.FilePath = urlFilePath;

            await WalksDataBaseContext.Images.AddAsync(image);
            await WalksDataBaseContext.SaveChangesAsync();

            return image;
        }
    }
}
