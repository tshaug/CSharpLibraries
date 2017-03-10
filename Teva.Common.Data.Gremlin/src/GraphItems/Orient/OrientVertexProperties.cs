using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    /// <summary>
    /// Orient-Implementation of IVertexProperties, derives from Dictionary
    /// </summary>
    public class OrientVertexProperties : Dictionary<string, List<IVertexValue>>, IVertexProperties
    {
        /// <summary>
        /// Sets a property with key and value
        /// </summary>
        /// <typeparam name="T">Generic value of property</typeparam>
        /// <param name="Key">Key of property</param>
        /// <param name="Value">Value of property</param>
        /// <param name="IgnoreDefaultValue">Ignoring the default value of other properties</param>
        public void SetProperty<T>(string Key, T Value, bool IgnoreDefaultValue = true)
        {
            if (Value is Enum)
                throw new Exception("Please cast Enum to base type before SetProperty");

            if (Value == null || (IgnoreDefaultValue && EqualityComparer<T>.Default.Equals(Value, default(T))))
                base.Remove(Key);
            else
                base[Key] = new List<IVertexValue> { new OrientVertexValue(Value) };
        }
        /// <summary>
        /// Sets a property with key and value
        /// </summary>
        /// <typeparam name="T">has no usage</typeparam>
        /// <param name="Key">Key of property</param>
        /// <param name="Value">String-Value of property</param>
        /// <param name="IgnoreDefaultValue">Ignoring the default value of other properties</param>
        public void SetProperty<T>(string Key, string Value, bool IgnoreDefaultValue = true)
        {
            if (Value == null || (IgnoreDefaultValue && Value.Length == 0))
                base.Remove(Key);
            else
                base[Key] = new List<IVertexValue> { new OrientVertexValue(Value) };
        }
        /// <summary>
        /// Removes a property with key
        /// </summary>
        /// <param name="Key">Key of property, which should be removed</param>
        public void RemoveProperty(string Key)
        {
            base.Remove(Key);
        }
        /// <summary>
        /// Gets a property with key
        /// </summary>
        /// <param name="Key">Key of wanted property</param>
        /// <returns>Wanted property</returns>
        public object GetProperty(string Key)
        {
            if (!base.ContainsKey(Key))
                return null;

            return base[Key][0].Contents;
        }
        /// <summary>
        /// Gets a property with key and returns a specific value
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="Key">Key of wanted property</param>
        /// <returns>Wanted property</returns>
        public T GetProperty<T>(string Key)
        {
            if (!base.ContainsKey(Key))
                return default(T);

            return (T)base[Key][0].Contents;
        }
        /// <summary>
        /// Gets a property with key und returns specific value
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="Key">Key of wanted property</param>
        /// <param name="DefaultValue">default value of T if key doesn't exists</param>
        /// <returns>Wanted property</returns>
        public T GetProperty<T>(string Key, T DefaultValue)
        {
            if (!base.ContainsKey(Key))
                return DefaultValue;

            return (T)base[Key][0].Contents;
        }
        /// <summary>
        /// Checks whether a property exists or not
        /// </summary>
        /// <param name="Key">Key of property</param>
        /// <returns>Whether property exists or not</returns>
        public bool HasProperty(string Key)
        {
            return base.ContainsKey(Key);
        }
    }
}
