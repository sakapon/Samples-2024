using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees
{
	// Test: https://atcoder.jp/contests/abc339/tasks/abc339_e
	class Merge_ABC339_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, d) = Read2();
			var a = Read();

			const int max = 500000;
			var monoid = new Monoid<int>((x, y) => x >= y ? x : y, 0);
			var st = new MergeTree<int>(monoid, max + 1);

			foreach (var v in a)
			{
				var l = v - d;
				var r = v + d;
				if (l < 0) l = 0;
				if (r > max) r = max;
				st[v] = Math.Max(st[v], st[l, r + 1] + 1);
			}
			return st[0, max + 1];
		}
	}
}
