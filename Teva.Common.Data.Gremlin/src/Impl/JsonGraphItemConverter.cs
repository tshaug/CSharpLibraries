using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teva.Common.Data.Gremlin.GraphItems;
using Teva.Common.Data.Gremlin.GraphItems.Orient;
using Teva.Common.Data.Gremlin.GraphItems.Titan;

namespace Teva.Common.Data.Gremlin.Impl
{
    public class JsonGraphItemConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IVertex) || objectType == typeof(IEdge) || objectType == typeof(IVertexValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var graphItemType = default(object);
            if (objectType == typeof(TitanVertexProperties) || objectType == typeof(OrientVertexProperties))
            {
                if (objectType == typeof(OrientVertexProperties))
                {
                    graphItemType = new OrientVertexProperties();
                }
                else if (objectType == typeof(TitanVertexProperties))
                {
                    graphItemType = new TitanVertexProperties();
                }
            }
            else if (objectType == typeof(TitanEdgeProperties) || objectType == typeof(OrientEdgeProperties))
            {
                if (objectType == typeof(OrientEdgeProperties))
                {
                    graphItemType = new OrientEdgeProperties();
                }
                else if (objectType == typeof(TitanEdgeProperties))
                {
                    graphItemType = new TitanEdgeProperties();
                }
            }
            else
            {
                JProperty jsonProp = jsonObject.Property("id");
                JToken token = jsonProp.Value;
                //Titan
                if (token.Type == JTokenType.Integer || token.Type == JTokenType.String)
                {
                    if (objectType == typeof(IVertex))
                    {
                        graphItemType = new TitanVertex();
                    }
                    else if (objectType == typeof(IEdge))
                    {
                        graphItemType = new TitanEdge();
                    }

                    else if (objectType == typeof(IVertexValue))
                    {
                        graphItemType = new TitanVertexValue();
                    }
                }
                //Orient
                else if (token.Type == JTokenType.Object)
                {
                    if (objectType == typeof(IVertex))
                    {
                        graphItemType = new OrientVertex();
                    }
                    else if (objectType == typeof(IEdge))
                    {
                        graphItemType = new OrientEdge();
                    }
                    else if (objectType == typeof(IVertexValue))
                    {
                        graphItemType = new OrientVertexValue();
                    }
                }
                else
                {
                    throw (new Exceptions.NotSupportedDatabaseException("Not supported GraphDatabase. Titan or OrientDB are supported."));
                }
            }
            serializer.Populate(jsonObject.CreateReader(), graphItemType);
            return graphItemType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}
