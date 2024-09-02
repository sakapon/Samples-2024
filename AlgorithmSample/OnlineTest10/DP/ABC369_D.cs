namespace OnlineTest10.DP
{
	// Test: https://atcoder.jp/contests/abc369/tasks/abc369_d
	class ABC369_D
	{
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var a = ReadL();
			Console.WriteLine(Solve1(n, a));
		}

		// 貰う
		static long Solve1(int n, long[] a)
		{
			var dp0 = new long[n + 1];
			var dp1 = new long[n + 1];
			dp1[0] = long.MinValue;

			for (int i = 0; i < n; i++)
			{
				dp0[i + 1] = Math.Max(dp0[i], dp1[i] + 2 * a[i]);
				dp1[i + 1] = Math.Max(dp0[i] + a[i], dp1[i]);
			}
			return Math.Max(dp0[n], dp1[n]);
		}

		// 貰う (メモリ節約)
		static long Solve2(int n, long[] a)
		{
			var dp0 = 0L;
			var dp1 = long.MinValue;

			for (int i = 0; i < n; i++)
			{
				(dp0, dp1) = (Math.Max(dp0, dp1 + 2 * a[i]), Math.Max(dp0 + a[i], dp1));
			}
			return Math.Max(dp0, dp1);
		}
	}
}
