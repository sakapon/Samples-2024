using AlgorithmLib10.Trees.LCAs101;

namespace OnlineTest10.Trees.LCAs
{
	// Test: https://atcoder.jp/contests/abc014/tasks/abc014_4
	class LCA_ABC014_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				map[v].Add(u);
			}

			var tour = new List<(int depth, int v)>();
			var depths = new int[n + 1];
			var first = new int[n + 1];
			DFS(1, -1, 0);

			var monoid = new Monoid<(int depth, int v)>((x, y) => x.depth <= y.depth ? x : y, (int.MaxValue, -1));
			var st = new SparseTable<(int depth, int v)>(tour.ToArray(), monoid);

			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				var l = first[a];
				var r = first[b];
				var (d0, _) = l <= r ? st[l, r + 1] : st[r, l + 1];
				return depths[a] + depths[b] - 2 * d0 + 1;
			}));

			void DFS(int v, int pv, int d)
			{
				depths[v] = d;
				first[v] = tour.Count;
				tour.Add((d, v));
				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					DFS(nv, v, d + 1);
					tour.Add((d, v));
				}
			}
		}
	}
}
