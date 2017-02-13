using System.Collections.Generic;
using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IEdgeProperties : IDictionary<string, object>
    {
        void SetProperty<T>(string Key, T Value, bool IgnoreDefaultValue = true);
        void SetProperty<T>(string Key, string Value, bool IgnoreDefaultValue = true);
        void RemoveProperty(string Key);
        object GetProperty(string Key);
        T GetProperty<T>(string Key);
        T GetProperty<T>(string Key, T DefaultValue);
        bool HasProperty(string Key);
    }
}
