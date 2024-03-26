using AlgorithmLib10.SegTrees.SegTrees214;

namespace OnlineTest10.SegTrees.Test04
{
	// Test: https://atcoder.jp/contests/past202212-open/tasks/past202212_l
	class Merge_PAST202212_L
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var monoid = new Monoid<int>((x, y) => x >= y ? x : y, 0);
			var st = new Int32MergeTree<int>(monoid);

			foreach (var (l, r) in ps.OrderBy(p => p.Item1).ThenBy(p => -p.Item2))
			{
				var v = st[r, 1 << 30] + 1;
				if (st[r] < v) st[r] = v;
			}
			return st[0, 1 << 30];
		}
	}
}
