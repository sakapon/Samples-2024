using System;

namespace OnlineTest10.DP
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/10/ALDS1_10_A
	class ALDS1_10_A_Rec
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());

			dp = new long[n + 1];
			Array.Fill(dp, -1);
			dp[1] = dp[0] = 1;
			Console.WriteLine(Rec(n));
		}

		// メモ化再帰
		static long[] dp;
		static long Rec(int n)
		{
			if (dp[n] != -1) return dp[n];
			return dp[n] = Rec(n - 1) + Rec(n - 2);
		}
	}
}
