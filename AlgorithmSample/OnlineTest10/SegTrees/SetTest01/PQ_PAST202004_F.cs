using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees.SetTest01
{
	// Test: https://atcoder.jp/contests/past202004-open/tasks/past202004_f
	class PQ_PAST202004_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			foreach (var (a, b) in ps) map[a].Add(b);

			var r = new int[n + 1];
			var set = new TreeMultiSet(101);

			for (int i = 1; i <= n; i++)
			{
				foreach (var b in map[i]) set.Add(b);

				var v = set.Count > 0 ? set.RemoveLast() : 0;
				r[i] = r[i - 1] + v;
			}
			return string.Join("\n", r[1..]);
		}
	}
}
