namespace rokka_client_c_sharp.Models;

public class RokkaResponseBody
{
    public int Total { get; set; } = 0;
    public List<ImageInfos> Items { get; set; } = new();
}