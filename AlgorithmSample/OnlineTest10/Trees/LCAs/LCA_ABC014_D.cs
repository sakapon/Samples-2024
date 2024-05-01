using AlgorithmLib10.Trees.LCAs101;

namespace OnlineTest10.Trees.LCAs
{
	// Test: https://atcoder.jp/contests/abc014/tasks/abc014_4
	class LCA_ABC014_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var tree = new LCATree(n + 1);
			tree.AddEdges(es, true);
			var lca = tree.Build(1);

			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				var d = lca.Depths[lca.GetLCA(a, b)];
				return lca.Depths[a] + lca.Depths[b] - 2 * d + 1;
			}));
		}
	}
}
