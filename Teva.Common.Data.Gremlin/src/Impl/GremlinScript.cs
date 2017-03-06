using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teva.Common.Data.Gremlin
{
    /// <summary>
    /// Stringbuilder for a Gremlin-Query
    /// </summary>
    public class GremlinScript
    {
        /// <summary>
        /// Initializes a new instance of GremlinScript with no content
        /// </summary>
        public GremlinScript()
        {
        }
        /// <summary>
        /// Initializes a new instance of GremlinScript with given Script
        /// </summary>
        /// <param name="Script">Script to append</param>
        public GremlinScript(string Script)
        {
            Append(Script);
        }
        /// <summary>
        /// gets metadata of current GremlinScript instance
        /// </summary>
        /// <param name="Key">key of metadata</param>
        /// <returns>meta value</returns>
        public object GetMetadata(string Key)
        {
            if (Metadata == null || !Metadata.ContainsKey(Key))
                return null;
            return Metadata[Key];
        }
        /// <summary>
        /// sets a new key-value-pair of metadata
        /// </summary>
        /// <param name="Key">key of metadata</param>
        /// <param name="Value">value of metadata</param>
        public void SetMetadata(string Key, object Value)
        {
            if (Metadata == null)
                Metadata = new Dictionary<string, object>();
            Metadata[Key] = Value;
        }
        private Dictionary<string, object> Metadata { get; set; }
        /// <summary>
        /// appends a given gremlin-statement at the tail of GremlinScript
        /// </summary>
        /// <param name="Script">script to append</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append(string Script)
        {
            SB.Append(Script);
            return this;
        }
        /// <summary>
        /// appends given gremlin-statements at the tail of GremlinScript
        /// </summary>
        /// <param name="Parts">scripts to append</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append(params object[] Parts)
        {
            return Insert(SB.Length, Parts);
        }
        /// <summary>
        /// inserts gremlin-statements on a specific position
        /// </summary>
        /// <param name="Index">position to insert</param>
        /// <param name="Parts">scripts to insert</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Insert(int Index, params object[] Parts)
        {
            var ToAppend = new StringBuilder();
            foreach (var Part in Parts)
                ToAppend.Append((string)Part);
            SB.Insert(Index, ToAppend.ToString());
            return this;
        }
        /// <summary>
        /// inserts gremlin-statements on a specific position in the root (first) gremlin-query
        /// </summary>
        /// <param name="Index">Index of position</param>
        /// <param name="Parts">Parts to insert</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript InsertRoot(int Index, params object[] Parts)
        {
            var ToAppend = new StringBuilder();
            foreach (var Part in Parts)
                ToAppend.Append((string)Part);
            InternalSB_GetRoot().Insert(Index, ToAppend.ToString());
            return this;
        }

        #region Internal SBs
        /// <summary>
        /// adds a new gremlin-query to GremlinScript
        /// </summary>
        public void InternalSB_Open()
        {
            if (StoredSBs == null)
                StoredSBs = new List<StringBuilder>();
            StoredSBs.Add(SB);
            SB = new StringBuilder();
        }
        /// <summary>
        /// closes current gremlin-query
        /// </summary>
        /// <returns>current gremlin-query</returns>
        public string InternalSB_Close()
        {
            if (StoredSBs == null || StoredSBs.Count == 0)
                throw new Exception("There's no stored String Builders");
            var ToReturn = SB.ToString();
            SB = StoredSBs[StoredSBs.Count - 1];
            StoredSBs.RemoveAt(StoredSBs.Count - 1);
            return ToReturn;
        }
        /// <summary>
        /// gets the first/root element of gremlin-queries 
        /// </summary>
        /// <returns>StringBuilder of root</returns>
        public StringBuilder InternalSB_GetRoot()
        {
            if (StoredSBs == null || StoredSBs.Count == 0)
                return SB;
            else
                return StoredSBs[0];
        }
        private List<StringBuilder> StoredSBs;
        private StringBuilder SB = new StringBuilder();
        #endregion
        /// <summary>
        /// gets the current script to string with bindings
        /// </summary>
        /// <returns>GremlinScript as string</returns>
        public string GetScript()
        {
            return SB.ToString();
        }
        /// <summary>
        /// gets the current script as string without bindings (readable). it's how the gremlin-query would be appear.
        /// </summary>
        /// <returns>readable script</returns>
        public string GetReadableScript()
        {
            string ToReturn = SB.ToString();
            for (int i = Parameters.Count - 1; i > -1; i--)
            {
                string NewValue;
                if (Parameters[i] is string)
                {
                    if (Parameters[i] == null)
                        NewValue = "''";
                    else
                        NewValue = "'" + ((string)Parameters[i]).Replace("'", "\\'") + "'";
                }
                else if (Parameters[i] is long)
                    NewValue = Parameters[i].ToString() + "L";
                else if (Parameters[i] is decimal || Parameters[i] is int || Parameters[i] is short)
                    NewValue = Parameters[i].ToString();
                else if (Parameters[i] is bool)
                    NewValue = Parameters[i].ToString().ToLower();
                else if (Parameters[i] is DateTime)
                    NewValue = (((DateTime)Parameters[i]).Ticks).ToString();
                else if (Parameters[i] == null)
                    NewValue = "null";
                else
                    NewValue = "?" + Parameters[i].ToString();
                ToReturn = ToReturn.Replace("p" + i, NewValue);
            }
            return ToReturn;
        }
        /// <summary>
        /// gets the bindings of the script, e.g. parameter in the has()-statement
        /// </summary>
        /// <returns>key-value-pairs of bindings</returns>
        public Dictionary<string, object> GetBindings()
        {
            var ToReturn = new Dictionary<string, object>();
            int Count = 0;
            foreach (var Parameter in Parameters)
                ToReturn.Add(string.Format("p{0}", Count++), Newtonsoft.Json.Linq.JToken.FromObject(Parameter));
            return ToReturn;
        }
        /// <summary>
        /// Returns wether GremlinScript is empty or not empty
        /// </summary>
        /// <returns>Boolean whether GremlinScript is empty or not</returns>
        public bool IsEmpty()
        {
            return SB.Length == 0;
        }
        /// <summary>
        /// Same as GetScript()
        /// </summary>
        /// <returns>Script to string</returns>
        public override string ToString()
        {
            return SB.ToString();
        }
        /// <summary>
        /// Gets Name of a parameter with given value
        /// </summary>
        /// <param name="Value">value of name</param>
        /// <returns>name of parameter</returns>
        public string GetParameterName(object Value)
        {
            int CurrentIndex = Parameters.FindIndex(T => T == Value);
            if (CurrentIndex != -1)
                return "p" + CurrentIndex;
            else
            {
                string ParameterName = "p" + Parameters.Count;
                Parameters.Add(Value);
                return ParameterName;
            }
        }
        private List<object> Parameters = new List<object>();
        
        /// <summary>
        /// Helper-Function for Groovy Statements
        /// </summary>
        /// <returns>Next variable Name for groovy</returns>
        public string GetNextVariableName()
        {
            VariableNameIndex++;
            return "x_" + VariableNameIndex;
        }
        private int VariableNameIndex = -1;
        /// <summary>
        /// gets id of the questioned element
        /// </summary>
        /// <returns>".id()" string</returns>
        public string Script_GetID()
        {
            return ".id()";
        }
        /// <summary>
        /// a helping method to append values of an abritary type
        /// </summary>
        /// <typeparam name="T">DataType to append</typeparam>
        /// <param name="Values">List of values to append</param>
        /// <param name="Seperator">"," as seperator</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Values<T>(IEnumerable<T> Values, string Seperator = ",")
        {
            bool First = true;
            foreach (var Value in Values)
            {
                if (First)
                    First = false;
                else
                    Append(Seperator);
                Append_Parameter(Value);
            }
            return this;
        }
        /// <summary>
        /// helper method to append properties in query
        /// </summary>
        /// <param name="Properties">Edge-Properties to append</param>
        /// <param name="AddCommaOnFirstItem">Appends comma on first item</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_PropertiesArrayString(Dictionary<string, object> Properties, bool AddCommaOnFirstItem = false)
        {
            if (Properties == null)
                return this;

            bool First = true;
            foreach (var Property in Properties)
            {
                if (Property.Value == null || Property.Value == null)
                    continue;
                if (First)
                {
                    First = false;
                    if (AddCommaOnFirstItem)
                        Append(",");
                }
                else
                    Append(",");
                Append_Parameter(Property.Key).Append(",").Append_Parameter(Property.Value);
            }
            return this;
        }
        /// <summary>
        /// helper method do append porperties in query
        /// </summary>
        /// <param name="Properties">Vertex-Properties to append</param>
        /// <param name="AddCommaOnFirstItem"></param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_PropertiesArrayString(Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool AddCommaOnFirstItem = false)
        {
            if (Properties == null)
                return this;

            bool First = true;
            foreach (var Property in Properties)
            {
                if (Property.Value == null || Property.Value.Count == 0 || Property.Value[0].Contents == null)
                    continue;
                if (Property.Value.Count != 1)
                    throw new Exception("Invalid Property.Value count: " + Property.Key);
                if (First)
                {
                    First = false;
                    if (AddCommaOnFirstItem)
                        Append(",");
                }
                else
                    Append(",");
                Append_Parameter(Property.Key).Append(",").Append_Parameter(Property.Value[0].Contents);
            }
            return this;
        }

        #region VertexExists
        /// <summary>
        /// appends g.V().has('IndexName','ID').hasNext(), Gremlin returns Boolean
        /// </summary>
        /// <param name="IndexName">key</param>
        /// <param name="ID">value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_VertexExistsByIndex(string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_HasNext();
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID').has(T.label,'Label').hasNext(), Gremlin returns Boolean
        /// </summary>
        /// <param name="Label">label</param>
        /// <param name="IndexName">key</param>
        /// <param name="ID">value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_VertexExistsByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_HasNext();
        }
        #endregion

        #region GetVertex/GetVertices
        /// <summary>
        /// appends g.V(ID), gets vertex by ID, Gremlin returns Vertex
        /// </summary>
        /// <param name="ID">ID of vertex</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVertex(string ID)
        {
            return Append("g.V(").Append_Parameter(ID).Append(")");
        }
        /// <summary>
        /// appends g.V(IDs[0],...,IDs[n]), gets vertices by IDs, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="IDs">List of IDs of vertices</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVertices(IEnumerable<string> IDs)
        {
            return Append("g.V(").Append_Values(IDs).Append(")");
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID'), gets vertices by property, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="IndexName">key</param>
        /// <param name="ID">value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVerticesByIndex(string IndexName, object ID)
        {
            return Append("g.V().has(").Append_Parameter(IndexName).Append(",").Append_Parameter(ID).Append(")");
        }
        /// <summary>
        /// appends g.V().has('IndexName',within('IDs[0]',...,'IDs[n])), gets vertices by key and values, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="IndexName">index</param>
        /// <param name="IDs">values</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVerticesByIndex(string IndexName, IEnumerable<object> IDs)
        {
            if (IDs.Count() == 1)
                return Append("g.V().has(").Append_Parameter(IndexName).Append(",").Append_Parameter(IDs.First()).Append(")");
            else
                return Append("g.V().has(").Append_Parameter(IndexName).Append(",within(").Append_Values(IDs).Append("))");
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID').has(T.label,'Label'), gets vertices by label, key and value, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">IndexName of wanted vertex</param>
        /// <param name="ID">ID of IndexName</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVerticesByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_FilterLabel(Label);
        }
        /// <summary>
        /// appends g.V().has('IndexName',within('IDs[0]',...,'IDs[n])).has(T.label,'Label'), gets vertices by label, key and values, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">IndexName of wanted vertex</param>
        /// <param name="IDs">IDs of IndexName</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable<object> IDs)
        {
            return Append_GetVerticesByIndex(IndexName, IDs).Append_FilterLabel(Label);
        }
        #endregion

        #region GetVertexID
        /// <summary>
        /// appends g.V().has('IndexName','ID'), gets vertices by property, Gremlin returns ids of vertices
        /// </summary>
        /// <param name="IndexName">key</param>
        /// <param name="ID">value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVertexIDByIndex(string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_ID();
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID').has(T.label,'Label'), gets vertices by label, key and value, Gremlin returns ids of vertices
        /// </summary>
        /// <param name="Label">Label of wanted vertex</param>
        /// <param name="IndexName">IndexName of wanted vertex</param>
        /// <param name="ID">ID of IndexName</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_ID();
        }
        #endregion

        #region GetEdge
        /// <summary>
        /// appends g.E(ID), gets edge by id, Gremlin returns wanted edge
        /// </summary>
        /// <param name="ID">ID of wanted edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge(string ID)
        {
            return Append("g.E(").Append_Parameter(ID).Append(")");
        }
        /// <summary>
        /// appends g.V(StartVertexID).bothE('EdgeName'), gets bidirected edge from StartVertex, Gremlin returns wanted List of edges
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge_Both(string StartVertexID, string EdgeName)
        {
            return Append_GetVertex(StartVertexID).Append_BothE(EdgeName);
        }
        /// <summary>
        /// appends g.V(StartVertexID).bothE('EdgeName'), gets outgoing edge from StartVertex, Gremlin returns wanted List of edges
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge_Out(string StartVertexID, string EdgeName)
        {
            return Append_GetVertex(StartVertexID).Append_OutE(EdgeName);
        }
        /// <summary>
        /// appends g.V(StartVertexID).inE('EdgeName'), gets ingoing edge from StarVertex, Germlin returns wanted List of edges
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge_In(string StartVertexID, string EdgeName)
        {
            return Append_GetVertex(StartVertexID).Append_InE(EdgeName);
        }
        /// <summary>
        /// appends g.V(StartVertexID).bothE('EdgeName').filter(inV().is(g.V(EndVertexID).next())), gets the bidirected edge with EdgeName between StartVertex and EndVertex
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge_Between_Both(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Both(StartVertexID, EdgeName).Append_FilterInVertex(EndVertexID);
        }
        /// <summary>
        /// appends g.V(StartVertexID).inE('EdgeName').filter(inV().is(g.V(EndVertexID).next())), gets the ingoing edge with EdgeName between StartVertex and EndVertex
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="EndVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge_Between_In(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_In(StartVertexID, EdgeName).Append_FilterInVertex(EndVertexID);
        }
        /// <summary>
        /// appends g.V(StartVertexID).outE('EdgeName').filter(inV().is(g.V(EndVertexID).next())), gets the outgoing edge with EdgeName between StartVertex and EndVertex
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetEdge_Between_Out(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Out(StartVertexID, EdgeName).Append_FilterInVertex(EndVertexID);
        }
        #endregion

        #region EdgeExists
        /// <summary>
        /// appends g.V(StartVertexID).bothE('EdgeName').hasNext(), determines whether a bidirected edge with EdgeName from StartVertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EdgeExistsBoth(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Both(StartVertexID, EdgeName).Append_HasNext();
        }
        /// <summary>
        /// appends g.V(StartVertexID).outE('EdgeName').hasNext(), determines whether an outgoing edge with EdgeName from StartVertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EdgeExistsOut(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Out(StartVertexID, EdgeName).Append_HasNext();
        }
        /// <summary>
        /// appends g.V(StartVertexID).inE('EdgeName').hasNext(), determines whether an ingoing edge with EdgeName from StartVertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EdgeExistsIn(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_In(StartVertexID, EdgeName).Append_HasNext();
        }
        /// <summary>
        /// appends g.V(StartVertexID).outE('EdgeName').filter(inV().is(g.V(EndVertexID).next())).hasNext(), determines whether a bidirected edge with EdgeName between StartVertex and EndVertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EdgeExistsBetweenBoth(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Between_Both(StartVertexID, EndVertexID, EdgeName).Append_HasNext();
        }
        /// <summary>
        /// appends g.V(StartVertexID).outE('EdgeName').filter(inV().is(g.V(EndVertexID).next())).hasNext(), determines whether an outgoing edge with EdgeName from StartVertex to EndVertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EdgeExistsBetweenOut(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Between_Out(StartVertexID, EndVertexID, EdgeName).Append_HasNext();
        }
        /// <summary>
        /// appends g.V(StartVertexID).inE('EdgeName').filter(inV().is(g.V(EndVertexID).next())).hasNext(), determines whether an ingoing edge with EdgeName from StartVertex to EndVertex exists
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="EndVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EdgeExistsBetweenIn(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Between_In(StartVertexID, EndVertexID, EdgeName).Append_HasNext();
        }
        #endregion

        #region CreateEdge
        /// <summary>
        /// appends g.V(StartVertexID).next().addEdge('Name',g.V(EndVertexID).next(),'key','value')), creates an outgoing edge with Name from StartVertex to EndVertex with Properties
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">Label of edge</param>
        /// <param name="Properties">Properties of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_CreateEdge(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVertex(StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVertex(EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        /// <summary>
        /// appends g.V().has('StartVertexIndexName','StartVertexID').next().addEdge('Name',g.V().has('EndVertexIndexName','EndVertexID').next(),'key','value')), creates an outgoing edge with Name from StartVertex to EndVertex with Properties, vertices are chosen by key-value-pairs
        /// </summary>
        /// <param name="StartVertexIndexName">Key of outgoing vertex</param>
        /// <param name="StartVertexID">Value of outgoing vertex</param>
        /// <param name="EndVertexIndexName">Key of ingoing Vertex</param>
        /// <param name="EndVertexID">Value of ingoing vertex</param>
        /// <param name="Name">Label of Edge</param>
        /// <param name="Properties">Properties of Edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_CreateEdge_Index(string StartVertexIndexName, object StartVertexID, string EndVertexIndexName, object EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVerticesByIndex(StartVertexIndexName, StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVerticesByIndex(EndVertexIndexName, EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        /// <summary>
        /// appends g.V().has('StartVertexIndexName','StartVertexID').next().addEdge('Name',g.V(EndVertexID).next(),'key','value')), creates an outgoing edge with Name from StartVertex by to EndVertex with Properties, vertices are chosen by key-value-pairs
        /// </summary>
        /// <param name="StartVertexIndexName">Key of outgoing vertex</param>
        /// <param name="StartVertexID">Value of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">Label of Edge</param>
        /// <param name="Properties">Properties of Edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_CreateEdge_StartIndex(string StartVertexIndexName, object StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVerticesByIndex(StartVertexIndexName, StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVertex(EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        /// <summary>
        /// appends g.V(StartVertexID).next().addEdge('Name',g.V().has('EndVertexIndexName','EndVertexID').next(),'key','value')), creates an outgoing edge with Name from StartVertex by to EndVertex with Properties, vertices are chosen by key-value-pairs
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexIndexName">Key of ingoing Vertex</param>
        /// <param name="EndVertexID">Value of ingoing vertex</param>
        /// <param name="Name">Label of Edge</param>
        /// <param name="Properties">Properties of Edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_CreateEdge_EndIndex(string StartVertexID, string EndVertexIndexName, object EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVertex(StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVerticesByIndex(EndVertexIndexName, EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        #endregion

        #region GetOrCreateEdge
        /// <summary>
        /// appends g.V(StartVertexID).outE('EdgeName').filter(inV().is(g.V(EndVertexID).next())).tryNext().orElseGet({g.V(StartVertexID).next().addEdge('Name',g.V(EndVertexID).next(),'key','value'))})
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="Name">Label of edge</param>
        /// <param name="Properties">Properties of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetOrCreateEdge_Out(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetEdge_Between_Out(StartVertexID, EndVertexID, Name).Append_TryNext().Append(".orElseGet({").Append_CreateEdge(StartVertexID, EndVertexID, Name, Properties).Append("})");
        }
        #endregion

        #region UpdateEdge
        /// <summary>
        /// appends a statement, that updates edge-properties of an Edge with ID and removes other properties if desired
        /// </summary>
        /// <param name="ID">ID of edge</param>
        /// <param name="Properties">Properties to update</param>
        /// <param name="RemoveOtherProperties">If Other Properties should be removed</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_UpdateEdge(string ID, Dictionary<string, object> Properties, bool RemoveOtherProperties)
        {
            string VariableName = GetNextVariableName();
            Append("def " + VariableName + "=").Append_GetEdge(ID).Append_Next().Append(";");
            if (RemoveOtherProperties)
            {
                Append(VariableName + ".properties().each{it.remove()};");
                foreach (var Property in Properties)
                {
                    if (Property.Value == null)
                        continue;
                    Append(VariableName).Append_SetProperty(Property.Key, Property.Value);
                }
            }
            else
            {
                foreach (var Property in Properties)
                    Append(VariableName).Append_SetProperty(Property.Key, Property.Value);
            }
            return this;
        }

        /// <summary>
        /// appends a statement, that updates edge-properties of an edge, that is a bidirected edge with EdgeName between StarVertex and EndVertex, and removes other properties if desired
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EndVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of edge</param>
        /// <param name="Properties">Properties of edge</param>
        /// <param name="RemoveOtherProperties">If Other Properties should be removed</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_UpdateEdgeBetween_Out(string StartVertexID, string EndVertexID, string EdgeName, Dictionary<string, object> Properties, bool RemoveOtherProperties)
        {
            string VariableName = GetNextVariableName();
            Append("def " + VariableName + "=").Append_GetEdge_Between_Out(StartVertexID, EndVertexID, EdgeName).Append_Next().Append(";");
            if (RemoveOtherProperties)
            {
                Append(VariableName + ".properties().each{it.remove()};");
                foreach (var Property in Properties)
                {
                    if (Property.Value == null)
                        continue;
                    Append(VariableName).Append_SetProperty(Property.Key, Property.Value);
                }
            }
            else
            {
                foreach (var Property in Properties)
                    Append(VariableName).Append_SetProperty(Property.Key, Property.Value);
            }
            return this;
        }
        #endregion

        #region DeleteEdge
        /// <summary>
        /// appends g.E(ID).sideEffect{it.get().remove()}.iterate(); deletes an edge with ID
        /// </summary>
        /// <param name="ID">ID of edge to delete</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteEdge(string ID)
        {
            return Append_GetEdge(ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        /// <summary>
        /// appends g.V(StartVertexID).bothE('EdgeName').sideEffect{it.get().remove()}.iterate(); deletes the bidirected edge with EdgeName
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of wanted edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteEdge_Both(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Both(StartVertexID, EdgeName).Append_DeletePipeGraphItems().Append_Iterate();
        }
        /// <summary>
        /// appends g.V(StartVertexID).outE('EdgeName').sideEffect{it.get().remove()}.iterate(); deletes the outgoing edge with EdgeName
        /// </summary>
        /// <param name="StartVertexID">ID of outgoing vertex</param>
        /// <param name="EdgeName">Label of wanted edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteEdge_Out(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Out(StartVertexID, EdgeName).Append_DeletePipeGraphItems().Append_Iterate();
        }
        /// <summary>
        /// appends g.V(StartVertexID).inE('EdgeName').sideEffect{it.get().remove()}.iterate(); deletes the ingoing edge with EdgeName
        /// </summary>
        /// <param name="StartVertexID">ID of ingoing vertex</param>
        /// <param name="EdgeName">Label of wanted edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteEdge_In(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_In(StartVertexID, EdgeName).Append_DeletePipeGraphItems().Append_Iterate();
        }
        #endregion

        #region CreateVertex
        /// <summary>
        /// appends g.addV('key','value'); creates a vertex with given Properties
        /// </summary>
        /// <param name="Properties">Key-Value-Pairs of vertex</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_CreateVertex(Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append("g.addV(").Append_PropertiesArrayString(Properties).Append(")").Append_Next();
        }
        /// <summary>
        /// appends g.addV(T.label,'Label','key','value'); creates a vertex with given Label and Properties
        /// </summary>
        /// <param name="Label">Lable of vertex</param>
        /// <param name="Properties">Key-Value-Pairs of vertex</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_CreateVertexWithLabel(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append("g.addV(T.label,").Append_Parameter(Label).Append(",").Append_PropertiesArrayString(Properties).Append(")").Append_Next();
        }
        #endregion

        #region GetOrCreateVertex
        /// <summary>
        /// appends g.V().has('IndexName','ID').tryNext().orElseGet({g.addV('key','value')}); gets vertex with IndexName and ID or creates Vertex with given properties if Vertex don't exists
        /// </summary>
        /// <param name="IndexName">Key of vertex</param>
        /// <param name="ID">Value of wanted key</param>
        /// <param name="Properties">Properties to create</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetOrCreateVertex(string IndexName, object ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_TryNext().Append(".orElseGet({").Append_CreateVertex(Properties).Append("})");
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID').has(T.label,'Label').tryNext().orElseGet({g.addV('key','value')}); gets vertex with Label, IndexName and ID or creates Vertex with given label and properties if Vertex don't exists
        /// </summary>
        /// <param name="Label">Label of vertex</param>
        /// <param name="IndexName">Key of wanted vertex</param>
        /// <param name="ID">Value of wanted vertex</param>
        /// <param name="Properties">Properties to create</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetOrCreateVertexWithLabel(string Label, string IndexName, object ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_TryNext().Append(".orElseGet({").Append_CreateVertexWithLabel(Label, Properties).Append("})");
        }
        #endregion

        #region UpdateVertex
        /// <summary>
        /// Updates the Properties of Vertex with ID, removes other properties if desired
        /// </summary>
        /// <param name="ID">ID of wanted vertex</param>
        /// <param name="Properties">Properties to update/create</param>
        /// <param name="RemoveOtherProperties">Removes or removes not other properties</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_UpdateVertex(string ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties, bool RemoveOtherProperties)
        {
            string VariableName = GetNextVariableName();
            Append("def " + VariableName + "=").Append_GetVertex(ID).Append_Next().Append(";");
            if (RemoveOtherProperties)
            {
                Append(VariableName + ".properties().each{it.remove()};");
                foreach (var Property in Properties)
                {
                    if (Property.Value == null || Property.Value.Count == 0 || Property.Value[0].Contents == null)
                        continue;
                    Append(VariableName).Append_SetProperty(Property.Key, Property.Value[0].Contents);
                }
            }
            else
            {
                foreach (var Property in Properties)
                    Append(VariableName).Append_SetProperty(Property.Key, Property.Value == null || Property.Value.Count == 0 ? null : Property.Value[0].Contents);
            }
            return this;
        }
        #endregion

        #region DeleteVertex
        /// <summary>
        /// appends g.V(ID).sideEffect{it.get().remove()}.iterate(), deletes Vertex with given ID
        /// </summary>
        /// <param name="ID">ID of wanted vertex to remove</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteVertex(string ID)
        {
            return Append_GetVertex(ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID').sideEffect{it.get().remove()}.iterate(), deletes Vertex with given Index and IndexValue(ID)
        /// </summary>
        /// <param name="IndexName">Key of wanted vertex to remove</param>
        /// <param name="ID">Value of wanted vertex to remove</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteVertexByIndex(string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        /// <summary>
        /// appends  g.V().has('IndexName','ID').has(T.label,'Label').sideEffect{it.get().remove()}.iterate(), deletes Vertex with given Label, Index and IndexValue(ID)
        /// </summary>
        /// <param name="Label">Label of wanted vertex to remove</param>
        /// <param name="IndexName">Key of wanted vertex to remove</param>
        /// <param name="ID">Value of wanted vertex to remove</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeleteVertexByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        #endregion

        #region In/Out/Both
        /// <summary>
        /// appends .in(), could be used on vertices, db returns a list of the ingoing vertices 
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_In()
        {
            return Append(".in()");
        }
        /// <summary>
        /// appends .in('Name'), can be used on vertices, db returns a list of the ingoing vertices with given edge-Name
        /// </summary>
        /// <param name="Name">Label of ingoing edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_In(string Name)
        {
            return Append(".in(").Append_Parameter(Name).Append(")");
        }
        /// <summary>
        /// appends .in('Names[0]',...,'Names[n]'), can be used on vertices, db returns a list of the ingoing vertices with given array of edge-Names
        /// </summary>
        /// <param name="Names">Labels of ingoing edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_In(string[] Names)
        {
            return Append(".in(").Append_Values(Names).Append(")");
        }
        /// <summary>
        /// appends .inE(), can be used on vertices, returns a list of outgoing edges 
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_InE()
        {
            return Append(".inE()");
        }
        /// <summary>
        /// appends .inE('Name'), can be used on vertices, db returns a list of outgoing edges with Name(Label)
        /// </summary>
        /// <param name="Name">Label of ingoing edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_InE(string Name)
        {
            return Append(".inE(").Append_Parameter(Name).Append(")");
        }
        /// <summary>
        /// appends .out(), can be used on vertices, db returns a list of the outgoing vertices 
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Out()
        {
            return Append(".out()");
        }
        /// <summary>
        /// appends .out('Name'), can be used on vertices, db returns a list of the outgoing vertices with given edge-Name
        /// </summary>
        /// <param name="Name">Label of outgoing edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Out(string Name)
        {
            return Append(".out(").Append_Parameter(Name).Append(")");
        }
        /// <summary>
        /// appends .out('Names[0]',...,'Names[n]'), can be used on vertices, db returns a list of the outgoing vertices with given array of edge-Names
        /// </summary>
        /// <param name="Names">Label of outgoing edges</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Out(string[] Names)
        {
            return Append(".out(").Append_Values(Names).Append(")");
        }
        /// <summary>
        /// appends .outE(), can be used on vertices, db returns a list of ingoing edges 
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_OutE()
        {
            return Append(".outE()");
        }
        /// <summary>
        /// appends .inE('Name'), can be used on vertices, db returns a list of ingoing edges with Name(Label)
        /// </summary>
        /// <param name="Name">Label of ingoing edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_OutE(string Name)
        {
            return Append(".outE(").Append_Parameter(Name).Append(")");
        }
        /// <summary>
        /// appends .inV(), can be used on edges, db returns a list of the ingoing vertices
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_InV()
        {
            return Append(".inV()");
        }
        /// <summary>
        /// appends .outV(), can be used on edges, db returns a list of the outgoing vertices
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_OutV()
        {
            return Append(".outV()");
        }
        /// <summary>
        /// appends .both(), can be used on vertices, db returns a list of outgoing and ingoing vertices
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Both()
        {
            return Append(".both()");
        }
        /// <summary>
        /// appends .both('Name'), can be used on vertices, db returns a list of the outgoing and ingoing vertices with label "Name"
        /// </summary>
        /// <param name="Name">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Both(string Name)
        {
            return Append(".both(").Append_Parameter(Name).Append(")");
        }
        /// <summary>
        /// appends .bothE(), can be used on vertices, db returns a list of the outgoing and ingoing edges
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_BothE()
        {
            return Append(".bothE()");
        }
        /// <summary>
        /// appends .bothE('Name'), can be used on vertices, db returns a list of the outgoing and ingoing edges with label "Name"
        /// </summary>
        /// <param name="Name">Label of edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_BothE(string Name)
        {
            return Append(".bothE(").Append_Parameter(Name).Append(")");
        }
        /// <summary>
        /// appends .bothV(), can be used on edges, db returns a list of the outgoing and ingoing vertices
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_BothV()
        {
            return Append(".bothV()");
        }
        #endregion

        #region Filters
        /// <summary>
        /// appends .has('Name') if Value==null and appends .not(has('Name','Value')) else, can be used on vertices or edges, db returns edges/vertices that have not the given value
        /// </summary>
        /// <param name="Name">Key of vertex/edge</param>
        /// <param name="Value">Value of vertex/edge</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterNotEqual(string Name, object Value)
        {
            if (Value == null)
                return Append(".has(").Append_Parameter(Name).Append(")");
            else
                return Append(".not(has(").Append_Parameter(Name).Append(",").Append_Parameter(Value).Append("))");
        }
        /// <summary>
        /// appends .is('Value'), can be used on properties, db returns wanted objects with Value
        /// </summary>
        /// <param name="Value">Value of object</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterEqual(object Value)
        {
            return Append(".is(").Append_Parameter(Value).Append(")");
        }
        /// <summary>
        /// appends .not(has('Name')) if Value==null and .has('Name','Value') else, can be used on vertices/edges, db returns vertices/edges that have the given Value
        /// </summary>
        /// <param name="Name">wanted Property-Key</param>
        /// <param name="Value">wanted Property-Value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterEqual(string Name, object Value)
        {
            if (Value == null)
                return Append(".not(has(").Append_Parameter(Name).Append("))");
            else
                return Append(".has(").Append_Parameter(Name).Append(",").Append_Parameter(Value).Append(")");
        }
        /// <summary>
        /// appends .is(within(Values[0],...,Values[n])), can be used on properties, db returns vertices/edges that have the given Values
        /// </summary>
        /// <param name="Values">wanted Property-Values</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterEquals(IEnumerable<object> Values)
        {
            return Append(".is(within(").Append_Values(Values).Append("))");
        }
        /// <summary>
        /// appends .has('Name',within(Values[0],...,Values[n])), can be used on vertices/edges, db returns vertices/edges that have the given Values with given Name
        /// </summary>
        /// <param name="Name">Wanted Property-Key</param>
        /// <param name="Values">Wanted Property-Values</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterEquals(string Name, IEnumerable<object> Values)
        {
            return Append(".has(").Append_Parameter(Name).Append(",within(").Append_Values(Values).Append("))");
        }
        /// <summary>
        /// appends .is(textRegex((?s)(?i).*(EscapeCodeOf(Value)).*)), can be used on properties, db returns vertices/edges where a value of a property contains the given Value 
        /// </summary>
        /// <param name="Value">Wanted Value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterContains(string Value)
        {
            return Append(".is(textRegex(").Append_Parameter("(?s)(?i).*(" + System.Text.RegularExpressions.Regex.Escape(Value) + ").*").Append("))");
        }
        /// <summary>
        /// appends .has('Name',textRegex((?s)(?i).*('EscapeCodeOf(Value)').*)), can be used on properties, db returns vertices/edges where a value with Name of a property contains the given Value 
        /// </summary>
        /// <param name="Name">Key of property</param>
        /// <param name="Value">Containing value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterContains(string Name, string Value)
        {
            return Append(".has(").Append_Parameter(Name).Append(",textRegex(").Append_Parameter("(?s)(?i).*(" + System.Text.RegularExpressions.Regex.Escape(Value) + ").*").Append("))");
        }
        /// <summary>
        /// appends .is(gte(Value)), can be used on properties, db returns vertices/edges where a value of a property is greater than equal the given Value
        /// </summary>
        /// <param name="Value">Value to compare</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterGreaterThanEquals(object Value)
        {
            return Append(".is(gte(").Append_Parameter(Value).Append("))");
        }
        /// <summary>
        /// appends .has('Name',gte(Value)), can be used on vertices/edges, db returns vertices/edges where the value with Key:"Name" of a property is greater than equal the given Value
        /// </summary>
        /// <param name="Name">Key of wanted property</param>
        /// <param name="Value">Value to compare</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterGreaterThanEquals(string Name, object Value)
        {
            return Append(".has(").Append_Parameter(Name).Append(",gte(").Append_Parameter(Value).Append("))");
        }
        /// <summary>
        /// appends .is(lte(Value)), can be used on properties, db returns vertices/edges where a value of a property is less than equal the given Value
        /// </summary>
        /// <param name="Value">Value to compare</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterLessThanEquals(object Value)
        {
            return Append(".is(lte(").Append_Parameter(Value).Append("))");
        }
        /// <summary>
        /// appends .has('Name',lte(Value)), can be used on vertices/edges, db returns vertices/edges where the value with Key:"Name" of a property is less than equal the given Value
        /// </summary>
        /// <param name="Name">Key of wanted property</param>
        /// <param name="Value">Value to compare</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterLessThanEquals(string Name, object Value)
        {
            return Append(".has(").Append_Parameter(Name).Append(",lte(").Append_Parameter(Value).Append("))");
        }
        /// <summary>
        /// appends .and(is(gte(From)),is(lte(To))), can be used on properties, db returns vertices/edges where a value of a property is between From and To
        /// </summary>
        /// <param name="From">Start value of range</param>
        /// <param name="To">End value of range</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterBetween(object From, object To)
        {
            Append(".and(");
            Append("is(gte(").Append_Parameter(From).Append("))");
            Append(",is(lte(").Append_Parameter(To).Append("))");
            Append(")");
            return this;
        }
        /// <summary>
        /// appends .has('Name',gte(From).and(lte(To)), can be used on vertices/edges, db returns vertices/edges where the value with Key:"Name" of a property is between From and To
        /// </summary>
        /// <param name="Name">Key of wanted property</param>
        /// <param name="From">Start value of range</param>
        /// <param name="To">End value of range</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterBetween(string Name, object From, object To)
        {
            return Append(".has(").Append_Parameter(Name).Append(",gte(").Append_Parameter(From).Append(").and(lte(").Append_Parameter(To).Append(")))");
        }
        /// <summary>
        /// appends .has(T.label,'Label'), can be used on vertices/edges, db returns vertices/edges with the given Label
        /// </summary>
        /// <param name="Label">Label of wanted object</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterLabel(string Label)
        {
            return Append(".has(T.label,").Append_Parameter(Label).Append(")");
        }
        /// <summary>
        /// appends .is(g.V(ID).next()), can be used on vertices, db returns whether the traversal is the vertex with ID or not
        /// </summary>
        /// <param name="ID">ID of vertex to check</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterIsVertex(string ID)
        {
            return Append(".is(").Append_GetVertex(ID).Append_Next().Append(")");
        }
        /// <summary>
        /// appends .filter(bothV().is(g.V(EndVertexID).next())), can be used on edges, db returns the edges which are outgoing and ingoing from vertex with EndVertexID
        /// </summary>
        /// <param name="EndVertexID">ID of vertex to check</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterBothVertex(string EndVertexID)
        {
            return Append(".filter(").Append("bothV()").Append_FilterIsVertex(EndVertexID).Append(")");
        }
        /// <summary>
        /// appends .filter(outV().is(g.V(EndVertexID).next())), can be used on edges, db returns the edges which are outgoing from vertex with EndVertexID
        /// </summary>
        /// <param name="EndVertexID">ID of vertex to check</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterOutVertex(string EndVertexID)
        {
            return Append(".filter(").Append("outV()").Append_FilterIsVertex(EndVertexID).Append(")");
        }
        /// <summary>
        /// appends .filter(inV().is(g.V(EndVertexID).next())), can be used on edges, db returns the edges which are ingoing from vertex with EndVertexID
        /// </summary>
        /// <param name="EndVertexID">ID of vertex to check</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FilterInVertex(string EndVertexID)
        {
            return Append(".filter(").Append("inV()").Append_FilterIsVertex(EndVertexID).Append(")");
        }
        #endregion

        #region Properties
        /// <summary>
        /// appends .values('Name')[0], can be used on vertices/edges, db returns the first property-value with property-Name
        /// </summary>
        /// <param name="Name">Key of Value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetProperty(string Name)
        {
            return Append(".values('" + Name + "')[0]");
        }
        /// <summary>
        /// appends .values('Name'), can be used on vertices/edges, db returns all porperty-values with property-Name
        /// </summary>
        /// <param name="Name">Key of values</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_GetProperties(string Name)
        {
            return Append(".values('" + Name + "')");
        }
        /// <summary>
        /// appends .property('Name').remove() if value==null and .property('Name','Value'), can be used on vertices/edges, db sets a new property with Name and Value
        /// </summary>
        /// <param name="Name">Key of value to set</param>
        /// <param name="Value">Value to to set</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_SetProperty(string Name, object Value)
        {
            // TOOD: concept?!
            if (Value == null)
                return Append(".property(" + GetParameterName(Name) + ").remove();");
            else
                return Append(".property(" + GetParameterName(Name) + ",").Append_Parameter(Value).Append(");");
        }
        /// <summary>
        /// appends .property('Name').remove(), can be used on vertices/edges, removes a property with Name from edges or vertices
        /// </summary>
        /// <param name="Name">Key of the property</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_RemoveProperty(string Name)
        {
            return Append(".property(" + GetParameterName(Name) + ").remove();");
        }
        #endregion

        /// <summary>
        /// appends .id(), can be used on vertices/edges/properties, db returns the id of the element/elements
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_ID()
        {
            return Append(".id()");
        }
        /// <summary>
        /// appends .valueMap(), can be used on vertices/edges, db returns a map of all properties
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_ValueMap()
        {
            return Append(".valueMap()");
        }

        #region Transforms
        /// <summary>
        /// appends .map, can be used on vertices/edges, maps something e.g. g.V(1).out().map {it.get().value('name')} is the same as g.V(1).out().values('name')
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_StartTransform()
        {
            return Append(".map");
        }
        /// <summary>
        /// appends .map{[ 
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_StartDictionaryTransform()
        {
            return Append(".map{[");
        }
        /// <summary>
        /// appends ]}
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_EndDictionaryTransform()
        {
            return Append("]}");
        }
        #endregion

        #region Next
        /// <summary>
        /// appends .next(), can be used on vertices/edges, db returns the current element of a list
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Next()
        {
            return Append(".next()");
        }
        /// <summary>
        /// appends .hasNext(), can be used on vertices/edges, db returns whether there is a element or not
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_HasNext()
        {
            return Append(".hasNext()");
        }
        /// <summary>
        /// appends .tryNext(), can be used on vertices/edges, db returns an optional (container object) which has vertices or is empty (composite of next() and tryNext())
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_TryNext()
        {
            return Append(".tryNext()");
        }
        #endregion

        /// <summary>
        /// appends .as('Name'), db provides a label to a step
        /// </summary>
        /// <param name="Name">name for step</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_As(string Name)
        {
            return Append(".as('" + Name + "')");
        }
        /// <summary>
        /// appends .select('Name'), db select labeled steps or objects out of a map
        /// </summary>
        /// <param name="Name">label of a step</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Back(string Name)
        {
            return Append(".select('" + Name + "')");
        }

        #region Aggregations
        /// <summary>
        /// appends .count, db counts all elements of the stream
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Count()
        {
            return Append(".count()");
        }
        /// <summary>
        /// appends .max(), db determines the largest number in the stream
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Max()
        {
            return Append(".max()");
        }
        /// <summary>
        /// appends .min(), db determines the smallest number in the stream
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Min()
        {
            return Append(".min()");
        }
        /// <summary>
        /// appends .sum(), db sums up a stream of numbers
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Sum()
        {
            return Append(".sum()");
        }
        #endregion

        /// <summary>
        /// appends .unfold(), db unrolls a iterable object (e.g. a List) in its linear form
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Unfold()
        {
            return Append(".unfold()");
        }
        /// <summary>
        /// appends .fold(), inverse operation to unfold, db folds a stream into a iterable object (e.g. a List)
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Fold()
        {
            return Append(".fold()");
        }
        /// <summary>
        /// appends .order().by(Name,decr/incr) if DefaulValue==null and .order.by(coalesce(values(Name),constant(DefaulValue)),decr/incr) else, db returns a sorted list of the properties
        /// </summary>
        /// <param name="Name">Key of property</param>
        /// <param name="DefaultValue">Default value to compare</param>
        /// <param name="Descending">Descending or Ascending</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_SortProperty(string Name, object DefaultValue = null, bool Descending = false)
        {
            if (DefaultValue == null)
                Append(".order().by(").Append_Parameter(Name).Append(", " + (Descending ? "decr" : "incr") + ")");
            else
                Append(".order().by(coalesce(values(").Append_Parameter(Name).Append("),constant(").Append_Parameter(DefaultValue).Append(")), " + (Descending ? "decr" : "incr") + ")");
            return this;
        }
        /// <summary>
        /// appends g.V(it.get())
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_ItToPipe()
        {
            return Append("g.V(it.get())");
        }
        /// <summary>
        /// appends g.E(it.get())
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_ItEdgeToPipe()
        {
            return Append("g.E(it.get())");
        }
        /// <summary>
        /// appends it.get()
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_ItGet()
        {
            return Append("it.get()");
        }
        /// <summary>
        /// appends .limit(Count) if From==0 and .range(From,(From+Count)) else, can be used on values, filters in a range
        /// </summary>
        /// <param name="From">Minimal value</param>
        /// <param name="Count">Maximal value</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Range(long From, long Count)
        {
            if (From == 0)
                return Append(".limit(" + Count + ")");
            else
                return Append(".range(" + From + "," + (From + Count) + ")");
        }
        /// <summary>
        /// appends .sideEffect{it.get().remove()}
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_DeletePipeGraphItems()
        {
            return Append(".sideEffect{it.get().remove()}");
        }
        /// <summary>
        /// appends .iterate()
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Iterate()
        {
            return Append(".iterate();");
        }
        /// <summary>
        /// appends [0], db returns first result
        /// </summary>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_FirstResult()
        {
            return Append("[0]");
        }
        /// <summary>
        /// appends constant([]) if Value==null and constant('Value') else, specifies a constant value (see "Append_SortProperty")
        /// </summary>
        /// <param name="Value">Constant value to output</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Constant(object Value)
        {
            if (Value == null)
                return Append("constant([])");
            else
                return Append("constant(").Append_Parameter(Value).Append(")");
        }
        /// <summary>
        /// appends parameter in script, helper method for appending parameteres
        /// </summary>
        /// <param name="Value">Parameter to append</param>
        /// <returns>Modified GremlinScript</returns>
        public GremlinScript Append_Parameter(object Value)
        {
            if (Value is long)
                Append("(long)");
            return Append(GetParameterName(Value));
        }
    }
}