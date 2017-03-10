using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    /// <summary>
    /// Orient-Implemenation of OrientGraphItem
    /// </summary>
    public abstract class OrientGraphItem : IGraphItem
    { 
        /// <summary>
        /// OrientGraphItem must have a special identification
        /// </summary>
        [JsonProperty("id")]
        public OrientGraphItemId id
        {
            get;
            set;
        }
        /// <summary>
        /// Every GraphItem has an ID
        /// </summary>
        public string ID
        {
            get { return id.ToString(); }
            set
            {
                id = new OrientGraphItemId();
                id.saveId(value);
            }
        }
    }
}
