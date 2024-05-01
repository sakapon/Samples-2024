
namespace AlgorithmLib10.Trees.LCAs101
{
	public class LCATree
	{
		readonly int n;
		readonly List<int>[] map;

		public LCATree(int n)
		{
			this.n = n;
			map = Array.ConvertAll(new bool[n], _ => new List<int>());
		}

		public void AddEdge(int u, int v, bool twoWay)
		{
			map[u].Add(v);
			if (twoWay) map[v].Add(u);
		}
		public void AddEdges(int[][] es, bool twoWay)
		{
			foreach (var e in es) AddEdge(e[0], e[1], twoWay);
		}
		public void AddEdges((int, int)[] es, bool twoWay)
		{
			foreach (var (u, v) in es) AddEdge(u, v, twoWay);
		}

		public LCAResult Build(int root)
		{
			var depths = new int[n];
			var tour = new List<(int, int)>();
			var steps = new int[n];
			Array.Fill(depths, -1);
			Array.Fill(steps, -1);
			DFS(root, -1, 0);
			return new LCAResult(depths, tour.ToArray(), steps);

			void DFS(int v, int pv, int d)
			{
				depths[v] = d;
				steps[v] = tour.Count;
				tour.Add((v, d));
				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					DFS(nv, v, d + 1);
					tour.Add((v, d));
				}
			}
		}
	}

	public class LCAResult
	{
		public int[] Depths { get; }
		public (int v, int depth)[] Tour { get; }

		// その頂点に初めて到着したときの歩数。
		readonly int[] steps;
		readonly SparseTable<(int v, int depth)> st;

		internal LCAResult(int[] depths, (int v, int depth)[] tour, int[] steps)
		{
			Depths = depths;
			Tour = tour;
			this.steps = steps;

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
