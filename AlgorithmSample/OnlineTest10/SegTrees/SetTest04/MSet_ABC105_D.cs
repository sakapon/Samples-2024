using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc105/tasks/abc105_d
	class MSet_ABC105_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();

			var s = 0L;
			var set = new Int32TreeMultiSet();
			set.Add(0);

			foreach (var x in a)
			{
				s += x;
				s %= m;
				set.Add((int)s);
			}
			return set.ToKeyCountArray().Sum(p => p.count * (p.count - 1) / 2);
		}

		static object Solve2()
		{
			var (n, m) = Read2();
			var a = Read();

			var s = 0L;
			var map = new Int32TreeMap<int>();
			map[0]++;

			foreach (var x in a)
			{
				s += x;
				s %= m;
				map[(int)s]++;
			}
			return map.ToArray().Sum(p => (long)p.value * (p.value - 1) / 2);
		}
	}
}
