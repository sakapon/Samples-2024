using AlgorithmLib10.Trees.ETs101;

namespace OnlineTest10.Trees.ETs
{
	// Test: https://atcoder.jp/contests/abc202/tasks/abc202_e
	class ET_ABC202_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Read();
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var root = 1;
			var tree = new ETTree(n + 1);
			for (int i = 0; i < n - 1; i++)
				tree.AddEdge(p[i], i + 2, false);
			var et = tree.Build(root);

			var etMap = Array.ConvertAll(new bool[n], _ => new List<int>());
			var tour = et.Tour;
			for (int i = 0; i < tour.Length; i++)
			{
				var v = tour[i];
				if (et.StepMap[v][0] == i)
				{
					etMap[et.Depths[v]].Add(i);
				}
			}

			return string.Join("\n", qs.Select(q =>
			{
				var (u, d) = q;
				var (l, r) = (et.StepMap[u][0], et.StepMap[u][^1]);
				return First(0, etMap[d].Count, i => etMap[d][i] > r) - First(0, etMap[d].Count, i => etMap[d][i] >= l);
			}));
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
