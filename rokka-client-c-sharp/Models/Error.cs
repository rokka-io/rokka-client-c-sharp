namespace rokka_client_c_sharp.Models;

public class Error
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool InvalidAuthentication { get; set; }
}