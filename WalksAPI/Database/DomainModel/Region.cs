using System;
using System.Collections.Generic;

namespace WalksAPI.Database.DomainModel;

public partial class Region
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? RegionImageUrl { get; set; }

    public virtual ICollection<Walk> Walks { get; set; } = new List<Walk>();
}
