namespace TreesLib.v101
{
	public class UndirectedTree
	{
		static readonly StringComparer FormComparer = StringComparer.Ordinal;

		static List<int>[] ToMap((int u, int v)[] edges)
		{
			var n = edges.Length + 1;
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in edges)
			{
				map[u].Add(v);
				map[v].Add(u);
			}
			return map;
		}

		readonly List<int>[] map;
		public UndirectedTree(List<int>[] map) => this.map = map;
		public UndirectedTree((int u, int v)[] edges) : this(ToMap(edges)) { }

		string DFS(int v, int parent)
		{
			var l = new List<string>();
			foreach (var nv in map[v])
			{
				if (nv == parent) continue;
				l.Add(DFS(nv, v));
			}
			l.Sort(FormComparer);
			var f = string.Join("", l);
			return $"({f})";
		}

		public string GetFormForVertex(int root)
		{
			return DFS(root, -1);
		}
	}
}
