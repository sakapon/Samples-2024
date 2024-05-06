
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
			var tour = new List<int>();
			Array.Fill(depths, -1);
			Array.Fill(parents, -1);
			Array.Fill(steps, -1);
			depths[root] = 0;
			DFS(root);
			return new LCAResult(depths, parents, steps, tour.ToArray());

			void DFS(int v)
			{
				steps[v] = tour.Count;
				tour.Add(v);
				foreach (var nv in map[v])
				{
					if (depths[nv] != -1) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					DFS(nv);
					tour.Add(v);
				}
			}
		}

		public LCAResult2 Build2(int root)
		{
			var depths = new int[n];
			var parents = new int[n];
			var stepMap = Array.ConvertAll(depths, _ => new List<int>());
			var tour = new List<int>();
			Array.Fill(depths, -1);
			Array.Fill(parents, -1);
			depths[root] = 0;
			DFS(root);
			return new LCAResult2(depths, parents, Array.ConvertAll(stepMap, l => l.ToArray()), tour.ToArray());

			void DFS(int v)
			{
				stepMap[v].Add(tour.Count);
				tour.Add(v);
				foreach (var nv in map[v])
				{
					if (depths[nv] != -1) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					DFS(nv);
					stepMap[v].Add(tour.Count);
					tour.Add(v);
				}
			}
		}
	}

	public class LCAResult
	{
		public int[] Depths { get; }
		public int[] Parents { get; }

		// その頂点に初めて到着したときの歩数。
		public int[] Steps { get; }
		public int[] Tour { get; }

		readonly SparseTable<int> st;

		internal LCAResult(int[] depths, int[] parents, int[] steps, int[] tour)
		{
			Depths = depths;
			Parents = parents;
			Steps = steps;
			Tour = tour;

			// depths[-1] は実行されません。
			var monoid = new Monoid<int>((x, y) => depths[x] <= depths[y] ? x : y, -1);
			st = new SparseTable<int>(tour, monoid);
		}

		public int GetLCA(int u, int v)
		{
			var s = Steps[u];
			var t = Steps[v];
			return s <= t ? st[s, t + 1] : st[t, s + 1];
		}
	}

	public class LCAResult2
	{
		public int[] Depths { get; }
		public int[] Parents { get; }
		public int[][] StepMap { get; }
		public int[] Tour { get; }

		readonly SparseTable<int> st;

		internal LCAResult2(int[] depths, int[] parents, int[][] stepMap, int[] tour)
		{
			Depths = depths;
			Parents = parents;
			StepMap = stepMap;
			Tour = tour;

			// depths[-1] は実行されません。
			var monoid = new Monoid<int>((x, y) => depths[x] <= depths[y] ? x : y, -1);
			st = new SparseTable<int>(tour, monoid);
		}

		public int GetLCA(int u, int v)
		{
			var s = StepMap[u][0];
			var t = StepMap[v][0];
			return s <= t ? st[s, t + 1] : st[t, s + 1];
		}
	}
}
