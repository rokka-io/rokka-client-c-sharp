namespace rokka_client_c_sharp.Models;

public class RokkaListResponseBody
{
    public int Total { get; set; } = 0;
    public string? Cursor { get; set; } = string.Empty;
    public Links? Links { get; set; } = null;
    public List<ImageInfos> Items { get; set; } = new();
}