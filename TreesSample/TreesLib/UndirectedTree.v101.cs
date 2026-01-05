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

		readonly (int u, int v)[] edges;
		readonly List<int>[] map;

		public UndirectedTree((int u, int v)[] edges)
		{
			this.edges = edges;
			map = ToMap(edges);
		}

		string GetFormByDFS(int v, int parent)
		{
			var l = new List<string>();
			foreach (var nv in map[v])
			{
				if (nv == parent) continue;
				l.Add(GetFormByDFS(nv, v));
			}
			l.Sort(FormComparer);
			var f = string.Join("", l);
			return $"({f})";
		}

		public string GetFormForVertex(int root)
		{
			return GetFormByDFS(root, -1);
		}

		public string GetFormForEdge(int u, int v)
		{
			var f1 = GetFormByDFS(u, v);
			var f2 = GetFormByDFS(v, u);
			if (FormComparer.Compare(f1, f2) > 0) (f1, f2) = (f2, f1);
			return f1 + f2;
		}

		public string GetFormForEdge(int root)
		{
			var (u, v) = edges[root];
			return GetFormForEdge(u, v);
		}

		public string GetNormalForm()
		{
			var depths = new int[map.Length];
			var parents = new int[map.Length];

			void DFS(int v, int parent)
			{
				foreach (var nv in map[v])
				{
					if (nv == parent) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					DFS(nv, v);
				}
			}

			void Reroot(int root)
			{
				depths[root] = 0;
				parents[root] = -1;
				DFS(root, -1);
			}

			Reroot(0);
			var tv = Array.IndexOf(depths, depths.Max());
			Reroot(tv);
			tv = Array.IndexOf(depths, depths.Max());

			var diameter = depths[tv];
			var radius = (diameter + 1) / 2;
			while (depths[tv] > radius) tv = parents[tv];
			if (diameter % 2 == 0)
				return GetFormForVertex(tv);
			else
				return GetFormForEdge(tv, parents[tv]);
		}
	}
}
