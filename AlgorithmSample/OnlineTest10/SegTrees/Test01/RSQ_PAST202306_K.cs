using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees.Test01
{
	// Test: https://atcoder.jp/contests/past15-open/tasks/past202306_k
	class RSQ_PAST202306_K
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Read().Prepend(0).ToArray();

			var p_ = new int[n + 1];
			for (int i = 1; i <= n; i++)
				p_[p[i]] = i;

			var rsq1 = new RSQTree(n + 1);
			var rsq2 = new RSQTree(n + 1);

			for (int i = 1; i <= n; i++)
			{
				rsq1.Add(p_[i], 1);
				rsq2.Add(p_[i], i);
			}

			var r = 0L;

			for (int i = 1; i <= n; i++)
			{
				r += rsq1[1, p_[i]] * i;
				r += rsq2[1, p_[i]];

				rsq1.Add(p_[i], -1);
				rsq2.Add(p_[i], -i);
			}
			return r;
		}
	}
}
