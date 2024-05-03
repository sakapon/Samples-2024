using AlgorithmLib10.Trees.ETs101;

namespace OnlineTest10.Trees.ETs
{
	// Test: https://atcoder.jp/contests/abc270/tasks/abc270_c
	class ET_ABC270_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, x, y) = Read3();
			var es = Array.ConvertAll(new bool[n - 1], _ => Read());

			var tree = new ETTree(n + 1, es, true);
			var et = tree.Build(y);

			var r = new List<int>();
			for (var v = x; v != -1; v = et.Parents[v])
				r.Add(v);
			return string.Join(" ", r);
		}
	}
}
