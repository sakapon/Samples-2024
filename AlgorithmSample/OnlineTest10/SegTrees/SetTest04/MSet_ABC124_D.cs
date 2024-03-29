using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc124/tasks/abc124_d
	class MSet_ABC124_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k) = Read2();
			var s = Console.ReadLine();

			var a = new int[n];
			a[0] = 1 - s[0] + '0';

			for (int i = 1; i < n; i++)
			{
				a[i] = a[i - 1];
				if (s[i] != s[i - 1]) a[i]++;
			}

			var set = new Int32TreeMultiSet();
			foreach (var x in a) set.Add(x);

			return Enumerable.Range(0, a[^1] + 1).Where(l => l % 2 == 0).Max(l => set.GetCount(l, l + 2 * k + 1));
		}
	}
}
