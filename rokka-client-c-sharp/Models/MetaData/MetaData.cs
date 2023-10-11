using System.Runtime.CompilerServices;
using rokka_client_c_sharp.Extensions;

namespace rokka_client_c_sharp.Models.MetaData;

public abstract class MetaData<T> : Dictionary<string, T>
{
    public virtual bool Suffixed => false;
    protected void Set(T value, [CallerMemberName]string name = "unknown")
    {
        Add(name.SnakeCase(), value);
    }

    protected T Get([CallerMemberName]string name = "unknown")
    {
        return this[name.SnakeCase()];
    }
}