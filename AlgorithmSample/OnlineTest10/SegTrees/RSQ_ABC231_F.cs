using AlgorithmLib10.SegTrees.SegTrees104;

namespace OnlineTest10.SegTrees
{
	// Test: https://atcoder.jp/contests/abc231/tasks/abc231_f
	class RSQ_ABC231_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();
			var b = Read();

			var r = 0L;
			var rsq = new Int32RSQTree();

			var q = Enumerable.Range(0, n).GroupBy(i => (a: a[i], b: b[i])).OrderBy(g => g.Key.a).ThenByDescending(g => g.Key.b);
			foreach (var g in q)
			{
				var v = g.Key.b;
				var c = g.LongCount();
				r += c * c;
				r += c * rsq[v, 1 << 30];
				rsq.Add(v, c);
			}
			return r;
		}
	}
}
