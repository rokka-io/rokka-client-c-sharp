using System.Collections.Concurrent;

namespace rokka_client_c_sharp.Models.MetaData;

public class CreateMetadata : MetaData<object>
{
    public MetaDataUser? MetaUser { get; set; }
    public MetaDataDynamic? MetaDynamic { get; set; }
    public MetaDataOptions? Options { get; set; }
}