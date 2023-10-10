using rokka_client_c_sharp.Models.MetaData;

namespace rokka_client_c_sharp.Models;

public class SourceImage
{
    public string Hash { get; set; } = string.Empty;
    public string ShortHash { get; set; } = string.Empty;
    public string BinaryHash { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Mimetype { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public int Size { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Organization { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public bool Protected { get; set; }
    public bool Locked { get; set; }
    public double Entropy { get; set; }
    public bool? Opaque { get; set; }
    public bool? Deleted { get; set; }
    public MetaData<object>? StaticMetadata { get; set; }
    public MetaDataDynamic? DynamicMetadata { get; set; }
    public MetaDataUser? UserMetadata { get; set; }
}