using AlgorithmLib10.SegTrees.SegTrees214;

namespace OnlineTest10.SegTrees.Test04
{
	// Test: https://atcoder.jp/contests/past16-open/tasks/past202309_n
	class Merge_PAST202309_N
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (qc, k) = Read2();
			var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine());

			var pows = MPows(k, qc);
			var sums = MSums(pows);

			var monoid = new Monoid<(long v, int c)>((x, y) => ((x.v + y.v * pows[x.c]) % M, x.c + y.c), (0, 0));
			var st = new Int32MergeTree<(long v, int c)>(monoid);
			var r = new List<long>();

			foreach (var q in qs)
			{
				var x = int.Parse(q[2..]);

				var c = st[x].c;
				if (q[0] == '+') c++;
				else c--;

				st[x] = (x * sums[c] % M, c);
				r.Add(st[0, 1 << 30].v);
			}
			return string.Join("\n", r);
		}

		const long M = 998244353;

		static long[] MPows(long b, int n)
		{
			var p = new long[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
			return p;
		}

		static long[] MSums(long[] a)
		{
			var n = a.Length;
			var s = new long[n + 1];
			for (int i = 0; i < n; ++i) s[i + 1] = (s[i] + a[i]) % M;
			return s;
		}
	}
}
