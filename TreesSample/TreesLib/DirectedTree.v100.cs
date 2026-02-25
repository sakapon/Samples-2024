namespace TreesLib.v100
{
	// 根付き木の場合のみ
	public static class DirectedTree
	{
		const string DOU = "+(", DOD = "-(", DO0 = "0(", DC = ")";
		static readonly StringComparer FormComparer = StringComparer.Ordinal;

		public static string GetFormForVertex((int u, int v)[] edges, int root)
		{
			var n = edges.Length + 1;
			var map = Array.ConvertAll(new bool[n], _ => new List<(int to, bool forward)>());
			foreach (var (u, v) in edges)
			{
				map[u].Add((v, true));
				map[v].Add((u, false));
			}
			return DFS(root, -1, false);

			string DFS(int v, int parent, bool forward)
			{
				var l = new List<string>();
				foreach (var (nv, nf) in map[v])
				{
					if (nv == parent) continue;
					l.Add(DFS(nv, v, nf));
				}
				l.Sort(FormComparer);
				var f = string.Join("", l);
				return $"{(parent == -1 ? DO0 : forward ? DOU : DOD)}{f}{DC}";
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
					case '(':
						++vi;
						if (q.Count > 0) edges.Add((q.Peek(), vi));
						q.Push(vi);
						break;
					case ')':
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
