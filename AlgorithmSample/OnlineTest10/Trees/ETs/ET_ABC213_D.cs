using AlgorithmLib10.Trees.ETs101;

namespace OnlineTest10.Trees.ETs
{
	// Test: https://atcoder.jp/contests/abc213/tasks/abc213_d
	class ET_ABC213_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read());

			var tree = new ETTree(n + 1, es, true);
			Array.ForEach(tree.Map, l => l.Sort());
			var et = tree.Build(1);
			return string.Join(" ", et.Tour);
		}
	}
}
