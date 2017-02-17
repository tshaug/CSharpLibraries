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
        /// <param name="Script"></param>
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
        /// <returns>extended GremlinScript</returns>
        public GremlinScript Append(string Script)
        {
            SB.Append(Script);
            return this;
        }
        /// <summary>
        /// appends given gremlin-statements at the tail of GremlinScript
        /// </summary>
        /// <param name="Parts">scripts to append</param>
        /// <returns>extended GremlinScript</returns>
        public GremlinScript Append(params object[] Parts)
        {
            return Insert(SB.Length, Parts);
        }
        /// <summary>
        /// inserts gremlin-statements on a specific position
        /// </summary>
        /// <param name="Index">position to insert</param>
        /// <param name="Parts">scripts to insert</param>
        /// <returns>modified GremlinScript</returns>
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
        /// <param name="Index"></param>
        /// <param name="Parts"></param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        public bool IsEmpty()
        {
            return SB.Length == 0;
        }
        /// <summary>
        /// Same as GetScript()
        /// </summary>
        /// <returns></returns>
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
        // TODO: discover sense behind this function 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNextVariableName()
        {
            VariableNameIndex++;
            return "x_" + VariableNameIndex;
        }
        private int VariableNameIndex = -1;
        /// <summary>
        /// gets id of the questioned element
        /// </summary>
        /// <returns></returns>
        public string Script_GetID()
        {
            return ".id()";
        }
        /// <summary>
        /// a helping method to append values of an abritary type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Values">List of values to append</param>
        /// <param name="Seperator">"," as seperator</param>
        /// <returns>modified GremlinScript</returns>
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
        /// <param name="Properties"></param>
        /// <param name="AddCommaOnFirstItem"></param>
        /// <returns>modified GremlinScript</returns>
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
        /// <param name="Properties"></param>
        /// <param name="AddCommaOnFirstItem"></param>
        /// <returns>modified GremlinScript</returns>
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
        /// <returns>modified GremlinScript</returns>
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
        /// <returns>modified GremlinScript</returns>
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
        /// <returns>modified GremlinScript</returns>
        public GremlinScript Append_GetVertex(string ID)
        {
            return Append("g.V(").Append_Parameter(ID).Append(")");
        }
        /// <summary>
        /// appends g.V(IDs[0],...,IDs[n]), gets vertices by IDs, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="IDs">List of IDs of vertices</param>
        /// <returns>modified GremlinScript</returns>
        public GremlinScript Append_GetVertices(IEnumerable<string> IDs)
        {
            return Append("g.V(").Append_Values(IDs).Append(")");
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID'), gets vertices by property, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="IndexName">key</param>
        /// <param name="ID">value</param>
        /// <returns>modified GremlinScript</returns>
        public GremlinScript Append_GetVerticesByIndex(string IndexName, object ID)
        {
            return Append("g.V().has(").Append_Parameter(IndexName).Append(",").Append_Parameter(ID).Append(")");
        }
        /// <summary>
        /// appends g.V().has('IndexName',within('IDs[0]',...,'IDs[n])), gets vertices by key and values, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="IndexName">index</param>
        /// <param name="IDs">values</param>
        /// <returns>modified GremlinScript</returns>
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
        /// <param name="Label"></param>
        /// <param name="IndexName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GremlinScript Append_GetVerticesByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_FilterLabel(Label);
        }
        /// <summary>
        /// appends g.V().has('IndexName',within('IDs[0]',...,'IDs[n])).has(T.label,'Label'), gets vertices by label, key and values, Gremlin returns List of Vertices
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="IndexName"></param>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public GremlinScript Append_GetVerticesByIndexAndLabel(string Label, string IndexName, IEnumerable<object> IDs)
        {
            return Append_GetVerticesByIndex(IndexName, IDs).Append_FilterLabel(Label);
        }
        #endregion

        #region GetVertexID
        /// <summary>
        /// appends g.V().has('IndexName','ID'), gets vertices by property, Gremlin returns ids of vertices
        /// </summary>
        /// <param name="IndexName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GremlinScript Append_GetVertexIDByIndex(string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_ID();
        }
        /// <summary>
        /// appends g.V().has('IndexName','ID').has(T.label,'Label'), gets vertices by label, key and value, Gremlin returns ids of vertices
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="IndexName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GremlinScript Append_GetVertexIDByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_ID();
        }
        #endregion

        #region GetEdge
        /// <summary>
        /// appends g.E(ID), gets edge by id, Gremlin returns wanted edge
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GremlinScript Append_GetEdge(string ID)
        {
            return Append("g.E(").Append_Parameter(ID).Append(")");
        }
        /// <summary>
        /// appends g.V(ID).bothE('EdgeName'), gets bidirected edge from StartVertex, Gremlin returns wanted List of edges
        /// </summary>
        /// <param name="StartVertexID"></param>
        /// <param name="EdgeName"></param>
        /// <returns></returns>
        public GremlinScript Append_GetEdge_Both(string StartVertexID, string EdgeName)
        {
            return Append_GetVertex(StartVertexID).Append_BothE(EdgeName);
        }
        /// <summary>
        /// appends g.V(ID).bothE('EdgeName'), gets outgoing edge from StartVertex, Gremlin returns wanted List of edges
        /// </summary>
        /// <param name="StartVertexID"></param>
        /// <param name="EdgeName"></param>
        /// <returns></returns>
        public GremlinScript Append_GetEdge_Out(string StartVertexID, string EdgeName)
        {
            return Append_GetVertex(StartVertexID).Append_OutE(EdgeName);
        }
        public GremlinScript Append_GetEdge_In(string StartVertexID, string EdgeName)
        {
            return Append_GetVertex(StartVertexID).Append_InE(EdgeName);
        }
        public GremlinScript Append_GetEdge_Between_Both(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Both(StartVertexID, EdgeName).Append_FilterInVertex(EndVertexID);
        }
        public GremlinScript Append_GetEdge_Between_In(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_In(StartVertexID, EdgeName).Append_FilterInVertex(EndVertexID);
        }
        public GremlinScript Append_GetEdge_Between_Out(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Out(StartVertexID, EdgeName).Append_FilterInVertex(EndVertexID);
        }
        #endregion

        #region EdgeExists
        public GremlinScript Append_EdgeExistsBoth(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Both(StartVertexID, EdgeName).Append_HasNext();
        }
        public GremlinScript Append_EdgeExistsOut(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Out(StartVertexID, EdgeName).Append_HasNext();
        }
        public GremlinScript Append_EdgeExistsIn(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_In(StartVertexID, EdgeName).Append_HasNext();
        }
        public GremlinScript Append_EdgeExistsBetweenBoth(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Between_Both(StartVertexID, EndVertexID, EdgeName).Append_HasNext();
        }
        public GremlinScript Append_EdgeExistsBetweenOut(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Between_Out(StartVertexID, EndVertexID, EdgeName).Append_HasNext();
        }
        public GremlinScript Append_EdgeExistsBetweenIn(string StartVertexID, string EndVertexID, string EdgeName)
        {
            return Append_GetEdge_Between_In(StartVertexID, EndVertexID, EdgeName).Append_HasNext();
        }
        #endregion

        #region CreateEdge
        public GremlinScript Append_CreateEdge(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVertex(StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVertex(EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        public GremlinScript Append_CreateEdge_Index(string StartVertexIndexName, object StartVertexID, string EndVertexIndexName, object EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVerticesByIndex(StartVertexIndexName, StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVerticesByIndex(EndVertexIndexName, EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        public GremlinScript Append_CreateEdge_StartIndex(string StartVertexIndexName, object StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVerticesByIndex(StartVertexIndexName, StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVertex(EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        public GremlinScript Append_CreateEdge_EndIndex(string StartVertexID, string EndVertexIndexName, object EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetVertex(StartVertexID).Append_Next().Append(".addEdge(").Append_Parameter(Name).Append(",").Append_GetVerticesByIndex(EndVertexIndexName, EndVertexID).Append_Next().Append_PropertiesArrayString(Properties, true).Append(");");
        }
        #endregion

        #region GetOrCreateEdge
        public GremlinScript Append_GetOrCreateEdge_Out(string StartVertexID, string EndVertexID, string Name, Dictionary<string, object> Properties = null)
        {
            return Append_GetEdge_Between_Out(StartVertexID, EndVertexID, Name).Append_TryNext().Append(".orElseGet({").Append_CreateEdge(StartVertexID, EndVertexID, Name, Properties).Append("})");
        }
        #endregion

        #region UpdateEdge
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
        public GremlinScript Append_DeleteEdge(string ID)
        {
            return Append_GetEdge(ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        public GremlinScript Append_DeleteEdge_Both(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Both(StartVertexID, EdgeName).Append_DeletePipeGraphItems().Append_Iterate();
        }
        public GremlinScript Append_DeleteEdge_Out(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_Out(StartVertexID, EdgeName).Append_DeletePipeGraphItems().Append_Iterate();
        }
        public GremlinScript Append_DeleteEdge_In(string StartVertexID, string EdgeName)
        {
            return Append_GetEdge_In(StartVertexID, EdgeName).Append_DeletePipeGraphItems().Append_Iterate();
        }
        #endregion

        #region CreateVertex
        public GremlinScript Append_CreateVertex(Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append("g.addV(").Append_PropertiesArrayString(Properties).Append(")").Append_Next();
        }
        public GremlinScript Append_CreateVertexWithLabel(string Label, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append("g.addV(T.label,").Append_Parameter(Label).Append(",").Append_PropertiesArrayString(Properties).Append(")").Append_Next();
        }
        #endregion

        #region GetOrCreateVertex
        public GremlinScript Append_GetOrCreateVertex(string IndexName, object ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_TryNext().Append(".orElseGet({").Append_CreateVertex(Properties).Append("})");
        }
        public GremlinScript Append_GetOrCreateVertexWithLabel(string Label, string IndexName, object ID, Dictionary<string, List<GraphItems.IVertexValue>> Properties)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_TryNext().Append(".orElseGet({").Append_CreateVertexWithLabel(Label, Properties).Append("})");
        }
        #endregion

        #region UpdateVertex
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
        public GremlinScript Append_DeleteVertex(string ID)
        {
            return Append_GetVertex(ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        public GremlinScript Append_DeleteVertexByIndex(string IndexName, object ID)
        {
            return Append_GetVerticesByIndex(IndexName, ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        public GremlinScript Append_DeleteVertexByIndexAndLabel(string Label, string IndexName, object ID)
        {
            return Append_GetVerticesByIndexAndLabel(Label, IndexName, ID).Append_DeletePipeGraphItems().Append_Iterate();
        }
        #endregion

        #region In/Out/Both
        public GremlinScript Append_In()
        {
            return Append(".in()");
        }
        public GremlinScript Append_In(string Name)
        {
            return Append(".in(").Append_Parameter(Name).Append(")");
        }
        public GremlinScript Append_In(string[] Names)
        {
            return Append(".in(").Append_Values(Names).Append(")");
        }

        public GremlinScript Append_InE()
        {
            return Append(".inE()");
        }
        public GremlinScript Append_InE(string Name)
        {
            return Append(".inE(").Append_Parameter(Name).Append(")");
        }

        public GremlinScript Append_Out()
        {
            return Append(".out()");
        }
        public GremlinScript Append_Out(string Name)
        {
            return Append(".out(").Append_Parameter(Name).Append(")");
        }
        public GremlinScript Append_Out(string[] Names)
        {
            return Append(".out(").Append_Values(Names).Append(")");
        }

        public GremlinScript Append_OutE()
        {
            return Append(".outE()");
        }
        public GremlinScript Append_OutE(string Name)
        {
            return Append(".outE(").Append_Parameter(Name).Append(")");
        }

        public GremlinScript Append_InV()
        {
            return Append(".inV()");
        }
        public GremlinScript Append_OutV()
        {
            return Append(".outV()");
        }

        public GremlinScript Append_Both()
        {
            return Append(".both()");
        }
        public GremlinScript Append_Both(string Name)
        {
            return Append(".both(").Append_Parameter(Name).Append(")");
        }

        public GremlinScript Append_BothE()
        {
            return Append(".bothE()");
        }
        public GremlinScript Append_BothE(string Name)
        {
            return Append(".bothE(").Append_Parameter(Name).Append(")");
        }

        public GremlinScript Append_BothV()
        {
            return Append(".bothV()");
        }
        #endregion

        #region Filters
        public GremlinScript Append_FilterNotEqual(string Name, object Value)
        {
            if (Value == null)
                return Append(".has(").Append_Parameter(Name).Append(")");
            else
                return Append(".not(has(").Append_Parameter(Name).Append(",").Append_Parameter(Value).Append("))");
        }
        public GremlinScript Append_FilterEqual(object Value)
        {
            return Append(".is(").Append_Parameter(Value).Append(")");
        }
        public GremlinScript Append_FilterEqual(string Name, object Value)
        {
            if (Value == null)
                return Append(".not(has(").Append_Parameter(Name).Append("))");
            else
                return Append(".has(").Append_Parameter(Name).Append(",").Append_Parameter(Value).Append(")");
        }
        public GremlinScript Append_FilterEquals(IEnumerable<object> Values)
        {
            return Append(".is(within(").Append_Values(Values).Append("))");
        }
        public GremlinScript Append_FilterEquals(string Name, IEnumerable<object> Values)
        {
            return Append(".has(").Append_Parameter(Name).Append(",within(").Append_Values(Values).Append("))");
        }
        public GremlinScript Append_FilterContains(string Value)
        {
            return Append(".is(textRegex(").Append_Parameter("(?s)(?i).*(" + System.Text.RegularExpressions.Regex.Escape(Value) + ").*").Append("))");
        }
        public GremlinScript Append_FilterContains(string Name, string Value)
        {
            return Append(".has(").Append_Parameter(Name).Append(",textRegex(").Append_Parameter("(?s)(?i).*(" + System.Text.RegularExpressions.Regex.Escape(Value) + ").*").Append("))");
        }
        public GremlinScript Append_FilterGreaterThanEquals(object Value)
        {
            return Append(".is(gte(").Append_Parameter(Value).Append("))");
        }
        public GremlinScript Append_FilterGreaterThanEquals(string Name, object Value)
        {
            return Append(".has(").Append_Parameter(Name).Append(",gte(").Append_Parameter(Value).Append("))");
        }
        public GremlinScript Append_FilterLessThanEquals(object Value)
        {
            return Append(".is(lte(").Append_Parameter(Value).Append("))");
        }
        public GremlinScript Append_FilterLessThanEquals(string Name, object Value)
        {
            return Append(".has(").Append_Parameter(Name).Append(",lte(").Append_Parameter(Value).Append("))");
        }
        public GremlinScript Append_FilterBetween(object From, object To)
        {
            Append(".and(");
            Append("is(gte(").Append_Parameter(From).Append("))");
            Append(",is(lte(").Append_Parameter(To).Append("))");
            Append(")");
            return this;
        }
        public GremlinScript Append_FilterBetween(string Name, object From, object To)
        {
            return Append(".has(").Append_Parameter(Name).Append(",gte(").Append_Parameter(From).Append(").and(lte(").Append_Parameter(To).Append(")))");
        }
        public GremlinScript Append_FilterLabel(string Label)
        {
            return Append(".has(T.label,").Append_Parameter(Label).Append(")");
        }
        public GremlinScript Append_FilterIsVertex(string ID)
        {
            return Append(".is(").Append_GetVertex(ID).Append_Next().Append(")");
        }
        public GremlinScript Append_FilterBothVertex(string EndVertexID)
        {
            return Append(".filter(").Append("bothV()").Append_FilterIsVertex(EndVertexID).Append(")");
        }
        public GremlinScript Append_FilterOutVertex(string EndVertexID)
        {
            return Append(".filter(").Append("outV()").Append_FilterIsVertex(EndVertexID).Append(")");
        }
        public GremlinScript Append_FilterInVertex(string EndVertexID)
        {
            return Append(".filter(").Append("inV()").Append_FilterIsVertex(EndVertexID).Append(")");
        }
        #endregion

        #region Properties
        public GremlinScript Append_GetProperty(string Name)
        {
            return Append(".values('" + Name + "')[0]");
        }
        public GremlinScript Append_GetProperties(string Name)
        {
            return Append(".values('" + Name + "')");
        }
        public GremlinScript Append_SetProperty(string Name, object Value)
        {
            if (Value == null)
                return Append(".property(" + GetParameterName(Name) + ").remove();");
            else
                return Append(".property(" + GetParameterName(Name) + ",").Append_Parameter(Value).Append(");");
        }
        public GremlinScript Append_RemoveProperty(string Name)
        {
            return Append(".property(" + GetParameterName(Name) + ").remove();");
        }
        #endregion

        public GremlinScript Append_ID()
        {
            return Append(".id()");
        }

        public GremlinScript Append_ValueMap()
        {
            return Append(".valueMap()");
        }

        #region Transforms
        public GremlinScript Append_StartTransform()
        {
            return Append(".map");
        }
        public GremlinScript Append_StartDictionaryTransform()
        {
            return Append(".map{[");
        }
        public GremlinScript Append_EndDictionaryTransform()
        {
            return Append("]}");
        }
        #endregion

        #region Next
        public GremlinScript Append_Next()
        {
            return Append(".next()");
        }
        public GremlinScript Append_HasNext()
        {
            return Append(".hasNext()");
        }
        public GremlinScript Append_TryNext()
        {
            return Append(".tryNext()");
        }
        #endregion

        public GremlinScript Append_As(string Name)
        {
            return Append(".as('" + Name + "')");
        }
        public GremlinScript Append_Back(string Name)
        {
            return Append(".select('" + Name + "')");
        }

        #region Aggregations
        public GremlinScript Append_Count()
        {
            return Append(".count()");
        }
        public GremlinScript Append_Max()
        {
            return Append(".max()");
        }
        public GremlinScript Append_Min()
        {
            return Append(".min()");
        }
        public GremlinScript Append_Sum()
        {
            return Append(".sum()");
        }
        #endregion

        public GremlinScript Append_Unfold()
        {
            return Append(".unfold()");
        }
        public GremlinScript Append_Fold()
        {
            return Append(".fold()");
        }
        public GremlinScript Append_SortProperty(string Name, object DefaultValue = null, bool Descending = false)
        {
            if (DefaultValue == null)
                Append(".order().by(").Append_Parameter(Name).Append(", " + (Descending ? "decr" : "incr") + ")");
            else
                Append(".order().by(coalesce(values(").Append_Parameter(Name).Append("),constant(").Append_Parameter(DefaultValue).Append(")), " + (Descending ? "decr" : "incr") + ")");
            return this;
        }
        public GremlinScript Append_ItToPipe()
        {
            return Append("g.V(it.get())");
        }
        public GremlinScript Append_ItEdgeToPipe()
        {
            return Append("g.E(it.get())");
        }
        public GremlinScript Append_ItGet()
        {
            return Append("it.get()");
        }
        public GremlinScript Append_Range(long From, long Count)
        {
            if (From == 0)
                return Append(".limit(" + Count + ")");
            else
                return Append(".range(" + From + "," + (From + Count) + ")");
        }
        public GremlinScript Append_DeletePipeGraphItems()
        {
            return Append(".sideEffect{it.get().remove()}");
        }
        public GremlinScript Append_Iterate()
        {
            return Append(".iterate();");
        }
        public GremlinScript Append_FirstResult()
        {
            return Append("[0]");
        }
        public GremlinScript Append_Constant(object Value)
        {
            if (Value == null)
                return Append("constant([])");
            else
                return Append("constant(").Append_Parameter(Value).Append(")");
        }

        public GremlinScript Append_Parameter(object Value)
        {
            if (Value is long)
                Append("(long)");
            return Append(GetParameterName(Value));
        }
    }
}