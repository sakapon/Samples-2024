
namespace AlgorithmLib10.Trees.ETs101
{
	[System.Diagnostics.DebuggerDisplay(@"N = {N}")]
	public class ETTree
	{
		readonly int n;
		public int N => n;

		readonly List<int>[] map;
		public List<int>[] Map => map;

		public ETTree(List<int>[] map)
		{
			n = map.Length;
			this.map = map;
		}

		public ETTree(int n)
		{
			this.n = n;
			map = Array.ConvertAll(new bool[n], _ => new List<int>());
		}
		public ETTree(int n, int[][] edges, bool twoWay) : this(n) { AddEdges(edges, twoWay); }
		public ETTree(int n, (int from, int to)[] edges, bool twoWay) : this(n) { AddEdges(edges, twoWay); }

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

		public ETResult Build(int root)
		{
			var depths = new int[n];
			var parents = new int[n];
			var stepMap = Array.ConvertAll(depths, _ => new List<int>());
			var tour = new List<int>();
			Array.Fill(depths, -1);
			Array.Fill(parents, -1);
			depths[root] = 0;
			DFS(root);
			return new ETResult(depths, parents, Array.ConvertAll(stepMap, l => l.ToArray()), tour.ToArray());

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

	public class ETResult
	{
		public int[] Depths { get; }
		public int[] Parents { get; }
		public int[][] StepMap { get; }
		public int[] Tour { get; }

		internal ETResult(int[] depths, int[] parents, int[][] stepMap, int[] tour)
		{
			Depths = depths;
			Parents = parents;
			StepMap = stepMap;
			Tour = tour;
		}
	}
}
