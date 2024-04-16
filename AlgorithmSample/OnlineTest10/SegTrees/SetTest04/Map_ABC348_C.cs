using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc348/tasks/abc348_c
	class Map_ABC348_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var map = new Int32TreeMap<int>();
			foreach (var (a, c) in ps)
				if (!map.TryGetValue(c, out var v) || v > a) map[c] = a;
			return map.ToArray().Max(p => p.value);
		}
	}
}
