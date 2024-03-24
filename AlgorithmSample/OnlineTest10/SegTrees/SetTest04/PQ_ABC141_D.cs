using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc141/tasks/abc141_d
	class PQ_ABC141_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var m = Read()[1];
			var a = Read();

			var set = new Int32TreeMultiSet();
			foreach (var x in a) set.Add(x);

			while (m-- > 0) set.Add(set.RemoveLast() / 2);
			return set.ToArray().Sum(x => (long)x);
		}
	}
}
