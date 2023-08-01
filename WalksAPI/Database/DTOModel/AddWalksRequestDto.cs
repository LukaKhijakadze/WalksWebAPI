using System.ComponentModel.DataAnnotations;
using WalksAPI.DataModels.Domain;

namespace WalksAPI.Database.DTOModel
{
    public class AddWalksRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0, 30)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public int DifficultyId { get; set; }
    }
}
