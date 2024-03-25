using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc073/tasks/abc073_c
	class Set_ABC073_C
	{
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var set = new Int32TreeSet();
			foreach (var x in a)
				if (!set.Add(x)) set.Remove(x);
			return set.Count;
		}

		static object Solve2()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			var map = new Int32TreeMap<int>();
			foreach (var x in a) map[x]++;
			return map.ToArray().Count(p => p.value % 2 == 1);
		}
	}
}
