namespace rokka_client_c_sharp.Models.MetaData;

public class Version: MetaData<string>
{
    public string Text {
        get => Get() as string;
        set => Set(value!);
    }
}