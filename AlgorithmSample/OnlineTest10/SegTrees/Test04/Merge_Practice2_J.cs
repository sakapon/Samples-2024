using AlgorithmLib10.SegTrees.SegTrees104;

namespace OnlineTest10.DataTrees.BSTs
{
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_j
	class Merge_Practice2_J
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main()
		{
			var (n, qc) = Read2();
			var a = Read();

			var st = new Int32MergeTree<int>(Monoid.Int32_Max);
			for (int i = 1; i <= n; i++) st[i] = a[i - 1];

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 1)
					st[q[1]] = q[2];
				else if (q[0] == 2)
					Console.WriteLine(st[q[1], q[2] + 1]);
				else
					Console.WriteLine(First(q[1], n + 1, j => st[q[1], j + 1] >= q[2]));
			}
			Console.Out.Flush();
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
