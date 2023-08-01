using WalksAPI.Database.DTO;
using WalksAPI.DataModels.Domain;

namespace WalksAPI.Database.DTOModel
{
    public class WalkDto
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public virtual DifficultyDto Difficulty { get; set; } = null!;

        public virtual RegionDto Region { get; set; } = null!;
    }
}
