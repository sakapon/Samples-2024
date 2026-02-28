namespace TreesLib.v102
{
	// 辺を根とする文字列表現を実装します。
	// 中心を葉から求めます。
	// Order: ()+-0<>[]{}
	public class DirectedTree
	{
		const string DOU = "+(", DOD = "-(", DO0 = "0(", DC = ")";
		static readonly StringComparer FormComparer = StringComparer.Ordinal;

		static List<(int to, bool forward)>[] ToMap((int u, int v)[] edges)
		{
			var n = edges.Length + 1;
			var map = Array.ConvertAll(new bool[n], _ => new List<(int to, bool forward)>());
			foreach (var (u, v) in edges)
			{
				map[u].Add((v, true));
				map[v].Add((u, false));
			}
			return map;
		}

		public readonly (int u, int v)[] edges;
		readonly List<(int to, bool forward)>[] map;

		public DirectedTree((int u, int v)[] edges)
		{
			this.edges = edges;
			map = ToMap(edges);
		}

		string GetFormByDFS(int v, int parent, bool forward)
		{
			var l = new List<string>();
			foreach (var (nv, nf) in map[v])
			{
				if (nv == parent) continue;
				l.Add(GetFormByDFS(nv, v, nf));
			}
			l.Sort(FormComparer);
			var f = string.Join("", l);
			return $"{(parent == -1 ? DO0 : forward ? DOU : DOD)}{f}{DC}";
		}

		public string GetFormForVertex(int root)
		{
			return GetFormByDFS(root, -1, false);
		}

		string GetFormForEdge(int u, int v)
		{
			var f1 = GetFormByDFS(u, v, true);
			var f2 = GetFormByDFS(v, u, false);
			if (FormComparer.Compare(f1, f2) > 0) (f1, f2) = (f2, f1);
			return f1 + f2;
		}

		public string GetFormForEdge(int root)
		{
			var (u, v) = edges[root];
			return GetFormForEdge(u, v);
		}

		int[] GetCenter()
		{
			var n = map.Length;

			var rem = n;
			var degrees = Array.ConvertAll(map, l => l.Count);
			var l = new List<int>();
			var lt = new List<int>();

			for (int v = 0; v < n; ++v)
				if (degrees[v] == 1) l.Add(v);

			while (rem > 2)
			{
				foreach (var v in l)
				{
					foreach (var (nv, _) in map[v])
					{
						if (degrees[nv] == 0) continue;
						--rem;
						--degrees[v];
						if (--degrees[nv] == 1) lt.Add(nv);
					}
				}
				(l, lt) = (lt, l);
				lt.Clear();
			}
			return l.ToArray();
		}

		public string GetNormalForm()
		{
			var center = GetCenter();
			if (center.Length == 1)
				return GetFormForVertex(center[0]);
			else
			{
				var (u, v) = (center[0], center[1]);
				if (!edges.Contains((u, v))) (u, v) = (v, u);
				return GetFormForEdge(u, v);
			}
		}

		// form: 標準形とは限りません。
		public static DirectedTree Parse(string form)
		{
			ArgumentNullException.ThrowIfNull(form);

			var edges = new List<(int, int)>();
			var roots = new List<int>();
			var vi = -1;
			var q = new Stack<int>();

			for (int si = 0; si < form.Length;)
			{
				if (StartsWith(form, si, DO0))
				{
					if (q.Count > 0) throw new FormatException();
					roots.Add(++vi);
					q.Push(vi);
					si += 2;
				}
				else if (StartsWith(form, si, DOU))
				{
					++vi;
					if (q.Count == 0) roots.Add(vi);
					else edges.Add((q.Peek(), vi));
					q.Push(vi);
					si += 2;
				}
				else if (StartsWith(form, si, DOD))
				{
					++vi;
					if (q.Count == 0) roots.Add(vi);
					else edges.Add((vi, q.Peek()));
					q.Push(vi);
					si += 2;
				}
				else if (StartsWith(form, si, DC))
				{
					if (q.Count == 0) throw new FormatException();
					q.Pop();
					si += 1;
				}
				else throw new FormatException();
			}

			if (q.Count > 0) throw new FormatException();
			if (roots.Count == 1)
			{
				return new DirectedTree(edges.ToArray());
			}
			if (roots.Count == 2)
			{
				var (u, v) = (roots[0], roots[1]);
				if (StartsWith(form, 0, DOD)) (u, v) = (v, u);
				edges.Add((u, v));
				return new DirectedTree(edges.ToArray());
			}
			throw new FormatException();
		}

		static bool StartsWith(string s, int index, string value)
		{
			for (int i = 0; i < value.Length; i++)
				if (index + i >= s.Length || s[index + i] != value[i]) return false;
			return true;
		}
	}
}
