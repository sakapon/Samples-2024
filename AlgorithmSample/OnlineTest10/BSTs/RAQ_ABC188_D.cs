using AlgorithmLib10.DataTrees.BSTs.BSTs104;

namespace OnlineTest10.DataTrees.BSTs
{
	// Test: https://atcoder.jp/contests/abc188/tasks/abc188_d
	class RAQ_ABC188_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, C) = Read2();
			var ps = Array.ConvertAll(new bool[n], _ => Read3());

			var raq = new Int32RAQTree();
			var set = new HashSet<int>();

			foreach (var (a, b, c) in ps)
			{
				raq.Add(a, b + 1, c);
				set.Add(a);
				set.Add(b + 1);
			}

			var xs = set.ToArray();
			Array.Sort(xs);
			var r = 0L;

			for (int i = 1; i < xs.Length; i++)
			{
				var x0 = xs[i - 1];
				var x1 = xs[i];
				r += (x1 - x0) * Math.Min(raq[x0], C);
			}
			return r;
		}
	}
}
