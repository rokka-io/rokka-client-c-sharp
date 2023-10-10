namespace rokka_client_c_sharp.Models.MetaData;

public class CreateOptions: MetaData<bool?>
{
    public bool? OptimizeSource {
        get => Get();
        set => Set(value!);
    }
}