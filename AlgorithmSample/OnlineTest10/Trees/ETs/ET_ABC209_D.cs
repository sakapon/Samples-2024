using AlgorithmLib10.Trees.ETs101;

namespace OnlineTest10.Trees.ETs
{
	// Test: https://atcoder.jp/contests/abc209/tasks/abc209_d
	class ET_ABC209_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, qc) = Read2();
			var es = Array.ConvertAll(new bool[n - 1], _ => Read());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var tree = new ETTree(n + 1, es, true);
			var et = tree.Build(1);
			return string.Join("\n", qs.Select(q => (et.Depths[q.Item1] + et.Depths[q.Item2]) % 2 == 0 ? "Town" : "Road"));
		}
	}
}
