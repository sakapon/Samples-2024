namespace TreesLib.v100
{
	// 根付き木の場合のみ
	public static class UndirectedTree
	{
		const char UO = '(', UC = ')';
		static readonly StringComparer FormComparer = StringComparer.Ordinal;

		public static string GetFormForVertex((int u, int v)[] edges, int root)
		{
			var n = edges.Length + 1;
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in edges)
			{
				map[u].Add(v);
				map[v].Add(u);
			}
			return DFS(root, -1);

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
				return $"{UO}{f}{UC}";
			}
		}

		public static (int u, int v)[] Parse(string form)
		{
			ArgumentNullException.ThrowIfNull(form);

			var edges = new List<(int, int)>();
			var vi = -1;
			var q = new Stack<int>();

			foreach (var c in form)
			{
				switch (c)
				{
					case UO:
						++vi;
						if (q.Count > 0) edges.Add((q.Peek(), vi));
						q.Push(vi);
						break;
					case UC:
						if (q.Count == 0) throw new FormatException();
						q.Pop();
						break;
					default:
						throw new FormatException();
				}
			}

			if (q.Count > 0) throw new FormatException();
			if ((edges.Count + 1) * 2 != form.Length) throw new FormatException();
			return edges.ToArray();
		}
	}
}
