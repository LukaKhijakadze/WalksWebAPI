using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalksAPI.Database.DomainModel;

public partial class Image
{
    public int Id { get; set; }

    [NotMapped]
    public IFormFile File { get; set; }
    public string FileName { get; set; } = null!;

    public string? FileDescription { get; set; }

    public string FileExtension { get; set; } = null!;

    public long FileSizeInBytes { get; set; }

    public string FilePath { get; set; } = null!;
}
