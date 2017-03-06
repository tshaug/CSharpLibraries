using System.Collections.Generic;
using Newtonsoft.Json;
using Teva.Common.Data.Gremlin.Impl;

namespace Teva.Common.Data.Gremlin.GraphItems
{
    /// <summary>
    /// Interface for EdgeProperties, is a derivation of a IDictionary
    /// </summary>
    [JsonConverter(typeof(JsonGraphItemConverter))]
    public interface IEdgeProperties : IDictionary<string, object>
    {
        /// <summary>
        /// Sets a property with key and value
        /// </summary>
        /// <typeparam name="T">Generic value of property</typeparam>
        /// <param name="Key">Key of property</param>
        /// <param name="Value">Value of property</param>
        /// <param name="IgnoreDefaultValue">Ignoring the default value of other properties</param>
        void SetProperty<T>(string Key, T Value, bool IgnoreDefaultValue = true);
        /// <summary>
        /// Sets a property with key and value
        /// </summary>
        /// <typeparam name="T">has no usage</typeparam>
        /// <param name="Key">Key of property</param>
        /// <param name="Value">String-Value of property</param>
        /// <param name="IgnoreDefaultValue">Ignoring the default value of other properties</param>
        void SetProperty<T>(string Key, string Value, bool IgnoreDefaultValue = true);
        /// <summary>
        /// Removes a property with key
        /// </summary>
        /// <param name="Key">Key of property, which should be removed</param>
        void RemoveProperty(string Key);
        /// <summary>
        /// Gets a property with key
        /// </summary>
        /// <param name="Key">Key of wanted property</param>
        /// <returns>Wanted property</returns>
        object GetProperty(string Key);
        /// <summary>
        /// Gets a property with key and returns a specific value
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="Key">Key of wanted property</param>
        /// <returns>Wanted property</returns>
        T GetProperty<T>(string Key);
        /// <summary>
        /// Gets a property with key und returns specific value
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="Key">Key of wanted property</param>
        /// <param name="DefaultValue">default value of T if key doesn't exists</param>
        /// <returns>Wanted property</returns>
        T GetProperty<T>(string Key, T DefaultValue);
        /// <summary>
        /// Checks whether a property exists or not
        /// </summary>
        /// <param name="Key">Key of property</param>
        /// <returns>Whether property exists or not</returns>
        bool HasProperty(string Key);
    }
}
