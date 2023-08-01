using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalksAPI.Database.DomainModel;
using WalksAPI.Database.DTOModel;
using WalksAPI.Services.IRepository;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public IImageRepository ImageRepository { get; }

        public ImagesController(IImageRepository imageRepository)
        {
            ImageRepository = imageRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateImage(imageUploadRequestDto);

            if(ModelState.IsValid)
            {
                var image = new Image()
                {
                    File = imageUploadRequestDto.File,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileName = imageUploadRequestDto.FileName,
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                };

                // Upload File
                await ImageRepository.Upload(image);

                return Ok(image);

            }

            return BadRequest(ModelState); 
        }

        private void ValidateImage(ImageUploadRequestDto request)
        {
            var validateExtensions = new List<string>() { ".png", ".jpeg", ".jpg" };

            if(!validateExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsuported File Extension");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File More Than 10MB");
            }
        }
    }   
}
