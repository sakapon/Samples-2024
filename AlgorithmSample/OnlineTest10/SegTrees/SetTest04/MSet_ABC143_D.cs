using AlgorithmLib10.SegTrees.SegTrees203;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc143/tasks/abc143_d
	class MSet_ABC143_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var l = Read();

			Array.Sort(l);
			var set = new Int32TreeMultiSet();
			foreach (var x in l) set.Add(x);

			var r = 0L;
			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					r += set.GetLastIndexLeq(l[i] + l[j] - 1) - j;
			return r;
		}
	}
}
