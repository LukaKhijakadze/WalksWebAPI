using System;
using System.Collections.Generic;

namespace WalksAPI.Database.DomainModel;

public partial class Walk
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double LengthInKm { get; set; }

    public string? WalkImageUrl { get; set; }

    public int RegionId { get; set; }

    public int DifficultyId { get; set; }

    public virtual Difficulty Difficulty { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;
}
