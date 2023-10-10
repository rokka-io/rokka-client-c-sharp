namespace rokka_client_c_sharp.Models;

public class SourceImagesListResponseBody
{
    public int Total { get; set; } = 0;
    public string? Cursor { get; set; } = string.Empty;
    public Links? Links { get; set; } = null;
    public List<SourceImage> Items { get; set; } = new();
}