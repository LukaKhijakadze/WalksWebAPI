using System;
using System.Collections.Generic;

namespace WalksAPI.Database.DomainModel;

public partial class Difficulty 
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Walk> Walks { get; set; } = new List<Walk>();
}
