using System.ComponentModel.DataAnnotations;

namespace WalksAPI.Database.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(3)]
        public string Code { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public string? RegionImageUrl { get; set; }

    }
}
