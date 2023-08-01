using WalksAPI.Database.DTOModel;

namespace WalksAPI.Database.DTO
{
    public class RegionDto
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? RegionImageUrl { get; set; }
    }
}
