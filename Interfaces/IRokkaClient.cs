using rokka_client_c_sharp.Models;

namespace rokka_client_c_sharp.Interfaces;

public interface IRokkaClient
{
    Task<RokkaResponse> CreateSourceImage(string fileName, byte[] bytes);
}