using Newtonsoft.Json;

namespace Teva.Common.Data.Gremlin.GraphItems.Orient
{
    public abstract class OrientGraphItem : IGraphItem
    { 
        [JsonProperty("id")]
        public OrientGraphItemId id
        {
            get;
            set;
        }

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
