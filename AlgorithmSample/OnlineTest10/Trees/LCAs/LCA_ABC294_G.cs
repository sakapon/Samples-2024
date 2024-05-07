using AlgorithmLib10.SegTrees.SegTrees111;
using AlgorithmLib10.Trees.LCAs101;

namespace OnlineTest10.Trees.LCAs
{
	// Test: https://atcoder.jp/contests/abc294/tasks/abc294_g
	class LCA_ABC294_G
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read3());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read3());

			var root = 1;
			var tree = new LCATree(n + 1);
			foreach (var (u, v, _) in es)
				tree.AddEdge(u, v, true);
			var lca = tree.Build(root);

			var rsq = new RSQTree(2 * n - 2);
			foreach (var e in es)
			{
				var (p, v, w) = e;
				if (lca.Parents[v] != p) (p, v) = (v, p);
				rsq[lca.StepMap[v][0] - 1] = w;
				rsq[lca.StepMap[v][^1]] = -w;
			}

			var r = new List<long>();
			foreach (var q in qs)
			{
				if (q.Item1 == 1)
				{
					var (_, j, w) = q;
					var (p, v, _) = es[j - 1];
					if (lca.Parents[v] != p) (p, v) = (v, p);
					rsq[lca.StepMap[v][0] - 1] = w;
					rsq[lca.StepMap[v][^1]] = -w;
				}
				else
				{
					var (_, u, v) = q;
					var p = lca.GetLCA(u, v);
					r.Add(rsq[lca.StepMap[p][0], lca.StepMap[u][0]] + rsq[lca.StepMap[p][0], lca.StepMap[v][0]]);
				}
			}
			return string.Join("\n", r);
		}
	}
}
