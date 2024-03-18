using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees.Test01
{
	// Test: https://atcoder.jp/contests/past202303-open/tasks/past202303_m
	class Merge_PAST202303_M
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();
			var b = Read();

			var st = new MergeTree<int>(m, Monoid.Int32_Max);
			for (int j = 0; j < m; j++) st[j] = b[j];

			for (int i = 0; i < n; i++)
			{
				var j = First(0, m, x => st[0, x + 1] >= a[i]);
				if (j == m) return $"No\n{i + 1}";
				st[j] -= a[i];
			}
			return "Yes\n" + string.Join(" ", Enumerable.Range(0, m).Select(j => b[j] - st[j]));
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
