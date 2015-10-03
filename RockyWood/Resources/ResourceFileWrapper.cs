using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using SuccincT.Options;

namespace RockyWood.Resources
{
    public class ResourceFileWrapper<T> : IResources
    {
        private static readonly Type TypeOfT = typeof(T);

        private readonly ResourceManager _resourceManager = GetResourceManager();

        private readonly Dictionary<Tuple<CultureInfo, string>, Option<string>> _resourceCache =
            new Dictionary<Tuple<CultureInfo, string>, Option<string>>();

        public Option<string> ResourceValue(string resource) => GetResourceValue(CultureInfo.InvariantCulture, resource);

        public Option<string> SpecificCultureResourceValue(CultureInfo culture, string resource) =>
            GetResourceValue(culture, resource);

        private static ResourceManager GetResourceManager() =>
            (ResourceManager)(TypeOfT.GetProperty("ResourceManager",
                                                  BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public)
                .GetValue(TypeOfT, null));

        private Option<string> GetResourceValue(CultureInfo culture, string resource)
        {
            var key = Tuple.Create(culture, resource);
            return _resourceCache.ContainsKey(key)
                ? _resourceCache[key]
                : CacheResource(key, _resourceManager.GetString(resource, culture));
        }

        private Option<string> CacheResource(Tuple<CultureInfo, string> key, string value)
        {
            var optionedValue = OptionizeValue(value);
            _resourceCache.Add(key, optionedValue);
            return optionedValue;
        }

        private static Option<string> OptionizeValue(string value) =>
            value != null ? Option<string>.Some(value) : Option<string>.None();
    }
}
