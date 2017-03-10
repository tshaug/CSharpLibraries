using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teva.Common.Data.Gremlin.GraphItems;
using Teva.Common.Data.Gremlin.GraphItems.Orient;
using Teva.Common.Data.Gremlin.GraphItems.Titan;

namespace Teva.Common.Data.Gremlin.Impl
{
    /// <summary>
    /// Json Converter Class to Convert the GraphItem-Interfaces in the right implementation
    /// </summary>
    public class JsonGraphItemConverter : JsonConverter
    {
        /// <summary>
        /// Field for Reading access (always true)
        /// </summary>
        public override bool CanRead => true;
        /// <summary>
        /// Field for Writing access (always false)
        /// </summary>
        public override bool CanWrite => false;
        /// <summary>
        /// Method to determine whether a Type is convertible
        /// </summary>
        /// <param name="objectType">Type to check</param>
        /// <returns>Whether Type is convertible</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IVertex) || objectType == typeof(IEdge) || objectType == typeof(IVertexValue);
        }
        /// <summary>
        /// Reads incoming Json and parses it to the right implementation
        /// </summary>
        /// <param name="reader">Reader that has json</param>
        /// <param name="objectType">objectType to which should be converted</param>
        /// <param name="existingValue">A potentially existing value</param>
        /// <param name="serializer">Serializer that is in use</param>
        /// <returns>parsed object</returns>
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
        /// <summary>
        /// Overrride WriteJson method (not needed, throws exception)
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">value to convert</param>
        /// <param name="serializer">Serializer that is in use</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}
