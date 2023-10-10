namespace rokka_client_c_sharp.Configuration;

public class RokkaConfiguration
{
    public string Key { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string RenderStack { get; set; } = string.Empty;

    public bool IsValid =>    !string.IsNullOrEmpty(Key) 
                           && !string.IsNullOrEmpty(Organization) 
                           && !string.IsNullOrEmpty(RenderStack);
}