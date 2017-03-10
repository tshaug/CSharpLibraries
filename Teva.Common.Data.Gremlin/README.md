#Teva Gremlin
##Effy Teva - C# Libraries

~~As Gremlin Server will hopefully soon be released,
I've created a C# client for accessing it, using .NET WebSockets.~~ Since a while Gremlin Server has been released.
Teva Gremlin is a C# client for accessing GremlinServer with .NET WebSockets. It is for [TinkerPop Version 3.x](tinkerpop.apache.org). 

**Please note this requires .NET Framework 4.6**

There are three primary classes:
* **GremlinClient** - Uses both GremlinScript and GremlinServerClient, to provide an easy to use library on Gremlin
```C#
using Teva.Common.Data.Gremlin;
using Teva.Common.Data.Gremlin.Impl;

class Program
{
	static void main(string args[])
	{
		IGremlinClient client = new GremlinClient("localhost");
		bool vertexExists = client.VertexExistsByIndex("Name","Effy");
	}
}
```
* **GremlinScript** - Gremlin script builder library, can be used to access a graph. 

Build Queries with functions:
```C#
IGremlinClient client = new GremlinClient("localhost");
GremlinScript script = new GremlinScript();
script.Append_VertexExistsByIndex("Name","Effy");
bool vertexExists = client.GetBoolean(script);
```
Or phrase Gremlin-Queries as a string (e.g. a specific statement isn't provided):
```C#
script.Append("g.V().has('Name','Effy').hasNext()");
bool vertexExists = client.GetBoolean(script);
```
* **GremlinServerClient** - WebSockets client, communicates directly with Gremlin Server

Provided interfaces:

* IGremlinClient, IGremlinServerClient - interface for the clients, e.g. for mocking purposes
* IGraphItems - You can use GraphItems to store data or access data, e.g. ids, labels or properties
```C#
IVertex vertex = client.GetVertexByIndex("Name","Effy");
Console.WriteLine(vertex.ID + " " + vertex.label);
```
* IGraph - vertices and edges are stored in a List. Also there are methods to add edges and vertices to the graph and the database. It can be used to maintain a graph locally. 
```C#
IGremlinClient client = new IGremlinClient("localhost");
IGraph graph = new TitanGraph(client);
IVertex vertexOut = graph.AddVertex("Person");
IVertex vertexIn = graph.AddVertex("Software");
graph.AddDirectedEdge("created", vertexOut, vertexIn);
```

Also implementations for OrientDB and Titan (and maybe for other TinkerPop-Related Graphdatabases) are provided. 