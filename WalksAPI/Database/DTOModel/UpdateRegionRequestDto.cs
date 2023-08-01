using System.ComponentModel.DataAnnotations;

namespace WalksAPI.Database.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public string? RegionImageUrl { get; set; }

    }
}
