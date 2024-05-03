
namespace AlgorithmLib10.Trees.LCAs101
{
	[System.Diagnostics.DebuggerDisplay(@"N = {N}")]
	public class LCATree
	{
		readonly int n;
		public int N => n;

		readonly List<int>[] map;
		public List<int>[] Map => map;

		public LCATree(List<int>[] map)
		{
			n = map.Length;
			this.map = map;
		}

		public LCATree(int n)
		{
			this.n = n;
			map = Array.ConvertAll(new bool[n], _ => new List<int>());
		}
		public LCATree(int n, int[][] edges, bool twoWay) : this(n) { AddEdges(edges, twoWay); }
		public LCATree(int n, (int from, int to)[] edges, bool twoWay) : this(n) { AddEdges(edges, twoWay); }

		public void AddEdge(int from, int to, bool twoWay)
		{
			map[from].Add(to);
			if (twoWay) map[to].Add(from);
		}
		public void AddEdges(int[][] edges, bool twoWay)
		{
			foreach (var e in edges) AddEdge(e[0], e[1], twoWay);
		}
		public void AddEdges((int from, int to)[] edges, bool twoWay)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public LCAResult Build(int root)
		{
			var depths = new int[n];
			var parents = new int[n];
			var steps = new int[n];
			var tour = new List<(int, int)>();
			Array.Fill(depths, -1);
			Array.Fill(parents, -1);
			Array.Fill(steps, -1);
			depths[root] = 0;
			DFS(root);
			return new LCAResult(depths, parents, steps, tour.ToArray());

			void DFS(int v)
			{
				steps[v] = tour.Count;
				tour.Add((v, depths[v]));
				foreach (var nv in map[v])
				{
					if (depths[nv] != -1) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					DFS(nv);
					tour.Add((v, depths[v]));
				}
			}
		}
	}

	public class LCAResult
	{
		public int[] Depths { get; }
		public int[] Parents { get; }
		public (int v, int depth)[] Tour { get; }

		// その頂点に初めて到着したときの歩数。
		readonly int[] steps;
		readonly SparseTable<(int v, int depth)> st;

		internal LCAResult(int[] depths, int[] parents, int[] steps, (int v, int depth)[] tour)
		{
			Depths = depths;
			Parents = parents;
			this.steps = steps;
			Tour = tour;

			var monoid = new Monoid<(int v, int depth)>((x, y) => x.depth <= y.depth ? x : y, (int.MaxValue, -1));
			st = new SparseTable<(int, int)>(tour, monoid);
		}

		public int GetLCA(int u, int v)
		{
			var s = steps[u];
			var t = steps[v];
			return s <= t ? st[s, t + 1].v : st[t, s + 1].v;
		}
	}
}
