using WalksAPI.Database.DomainModel;

namespace WalksAPI.Services.IRepository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
