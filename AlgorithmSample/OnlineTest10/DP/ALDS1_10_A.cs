using System;

namespace OnlineTest10.DP
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/10/ALDS1_10_A
	class ALDS1_10_A
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			Console.WriteLine(Fibonacci1(n));
		}

		// 貰う
		static long Fibonacci1(int n)
		{
			var dp = new long[n + 1];
			dp[1] = dp[0] = 1;
			for (int i = 2; i <= n; i++)
			{
				dp[i] = dp[i - 1] + dp[i - 2];
			}
			return dp[n];
		}

		// 配る
		static long Fibonacci2(int n)
		{
			var dp = new long[n + 2];
			dp[0] = 1;
			for (int i = 0; i < n; i++)
			{
				dp[i + 1] += dp[i];
				dp[i + 2] += dp[i];
			}
			return dp[n];
		}

		// 貰う (メモリ節約)
		static long Fibonacci3(int n)
		{
			if (n <= 1) return 1;
			long v0 = 1, v1 = 1, v2 = 0;
			for (int i = 2; i <= n; i++)
			{
				v2 = v0 + v1;
				v0 = v1;
				v1 = v2;
			}
			return v1;
		}

		// 配る (メモリ節約)
		static long Fibonacci4(int n)
		{
			if (n <= 1) return 1;
			long v0 = 1, v1 = 0, v2 = 0;
			for (int i = 0; i < n; i++)
			{
				v2 = v0;
				v0 += v1;
				v1 = v2;
			}
			return v0;
		}
	}
}
