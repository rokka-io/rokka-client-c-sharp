namespace rokka_client_c_sharp.Models.MetaData;

public class MetaDataOptions : MetaData<object>
{
    public bool? VisualBinaryhash {
        get => Get() as bool?;
        set => Set(value!);
    }
    public bool? Protected {
        get => Get() as bool?;
        set => Set(value!);
    }
}