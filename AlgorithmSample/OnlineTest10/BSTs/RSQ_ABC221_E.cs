using AlgorithmLib10.DataTrees.BSTs.BSTs104;

namespace OnlineTest10.DataTrees.BSTs
{
	// Test: https://atcoder.jp/contests/abc221/tasks/abc221_e
	class RSQ_ABC221_E
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();

			var p2 = MPows(2, n);
			var p2_ = MPows(MHalf, n);

			var r = 0L;
			var rsq = new Int32RSQTree();

			for (int i = n - 1; i >= 0; i--)
			{
				r += rsq[a[i], 1 << 30] % M * p2_[i + 1];
				r %= M;
				rsq.Add(a[i], p2[i]);
			}
			return r;
		}

		const long M = 998244353;
		const long MHalf = (M + 1) / 2;

		static long[] MPows(long b, int n)
		{
			var p = new long[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
			return p;
		}
	}
}
