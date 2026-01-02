namespace TreesLib.v100
{
	public static class UndirectedTree
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

		static string GetFormForVertex(List<int>[] map, int root)
		{
			return DFS(root, -1);

			string DFS(int v, int pv)
			{
				var l = new List<string>();
				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					l.Add(DFS(nv, v));
				}
				l.Sort(FormComparer);
				var f = string.Join("", l);
				return $"({f})";
			}
		}

		public static string GetFormForVertex((int u, int v)[] edges, int root)
		{
			var map = ToMap(edges);
			return GetFormForVertex(map, root);
		}
	}
}
