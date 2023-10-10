using System.Collections.Concurrent;

namespace rokka_client_c_sharp.Models.MetaData;

public class CreateMetadata : MetaData<object>
{
    public MetaDataUser? MetaUser 
    {
        get => Get() as MetaDataUser;
        set => Set(value!);
    }
    public MetaDataDynamic? MetaDynamic 
    {
        get => Get() as MetaDataDynamic;
        set => Set(value!);
    }
    public MetaDataOptions? Options
    {
        get => Get() as MetaDataOptions;
        set => Set(value!);
    }
}