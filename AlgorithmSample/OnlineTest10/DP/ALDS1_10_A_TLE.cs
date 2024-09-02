using System;

namespace OnlineTest10.DP
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/10/ALDS1_10_A
	class ALDS1_10_A_TLE
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());

			Console.WriteLine(Rec(n));
		}

		// TLE
		static long Rec(int n)
		{
			if (n <= 1) return 1;
			return Rec(n - 1) + Rec(n - 2);
		}
	}
}
