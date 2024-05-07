using AlgorithmLib10.Trees.ETs101;

namespace OnlineTest10.Trees.ETs
{
	// Test: https://atcoder.jp/contests/abc267/tasks/abc267_f
	class ET_ABC267_F
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

			var rn = Enumerable.Range(1, n).ToArray();

			var tree = new ETTree(n + 1, es, true);
			var et = tree.Build(1);
			var rv1 = rn.MaxBy(v => et.Depths[v]);
			var et1 = tree.Build(rv1);
			var rv2 = rn.MaxBy(v => et1.Depths[v]);
			var et2 = tree.Build(rv2);

			var qmap = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			for (int qi = 0; qi < qc; qi++)
				qmap[qs[qi].Item1].Add(qi);

			var dqmap = Array.ConvertAll(new bool[n], _ => new List<int>());

			var r = new int[qc];
			Array.Fill(r, -1);
			Try(rv1, et1.Depths);
			Try(rv2, et2.Depths);
			return string.Join("\n", r);

			void Try(int root, int[] depths)
			{
				DFS(root, -1);

				void DFS(int v, int pv)
				{
					foreach (var nv in tree.Map[v])
					{
						if (nv == pv) continue;
						DFS(nv, v);
					}

					foreach (var qi in qmap[v])
					{
						var d = depths[v] - qs[qi].Item2;
						if (d >= 0) dqmap[d].Add(qi);
					}

					foreach (var qi in dqmap[depths[v]])
					{
						r[qi] = v;
					}
					dqmap[depths[v]].Clear();
				}
			}
		}
	}
}
