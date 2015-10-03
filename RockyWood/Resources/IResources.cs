using System.Globalization;
using SuccincT.Options;

namespace RockyWood.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResources
    {
        Option<string> ResourceValue(string resource);
        Option<string> SpecificCultureResourceValue(CultureInfo culture, string resource);
    }
}
