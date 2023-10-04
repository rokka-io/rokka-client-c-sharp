namespace rokka_client_c_sharp.Models;

public class ImageInfos
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
}