

namespace rokka_client_c_sharp.Models.MetaData;

public class MetaDataDynamic : MetaData<object>
{
    public Version? Version {
        get => Get() as Version;
        set => Set(value!);
    }
    
    public Area? SubjectArea {
        get => Get() as Area;
        set => Set(value!);
    }

    public Area? CropArea
    {
        get => Get() as Area;
        set => Set(value!);
    }
}