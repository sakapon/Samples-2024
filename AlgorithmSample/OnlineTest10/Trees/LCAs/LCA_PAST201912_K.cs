using AlgorithmLib10.Trees.LCAs101;

namespace OnlineTest10.Trees.LCAs
{
	// Test: https://atcoder.jp/contests/past201912-open/tasks/past201912_k
	class LCA_PAST201912_K
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var p = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var tree = new LCATree(n + 1);
			var root = 0;

			for (int i = 0; i < n; i++)
			{
				if (p[i] == -1)
					root = i + 1;
				else
					tree.AddEdge(p[i], i + 1, false);
			}
			var lca = tree.Build(root);

			return string.Join("\n", qs.Select(q =>
			{
				var (a, b) = q;
				return lca.GetLCA(a, b) == b ? "Yes" : "No";
			}));
		}
	}
}
