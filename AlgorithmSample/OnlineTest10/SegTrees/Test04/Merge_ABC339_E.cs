using AlgorithmLib10.SegTrees.SegTrees104;

namespace OnlineTest10.SegTrees.Test04
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

			var monoid = new Monoid<int>((x, y) => x >= y ? x : y, 0);
			var st = new Int32MergeTree<int>(monoid);

			foreach (var v in a)
			{
				var nv = st[v - d, v + d + 1] + 1;
				if (st[v] < nv) st[v] = nv;
			}
			return st[0, 1 << 30];
		}
	}
}
