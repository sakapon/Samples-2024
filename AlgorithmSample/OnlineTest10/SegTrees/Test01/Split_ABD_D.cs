using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees.Test01
{
	// Test: https://atcoder.jp/contests/abl/tasks/abl_d
	class Split_ABD_D
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, k) = Read2();
			var a = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

			const int max = 300000;
			var monoid = new Monoid<int>((x, y) => x >= y ? x : y, 0);
			var st = new SplitTree<int>(max + 1, monoid);

			foreach (var v in a)
			{
				st.Set(v - k, v + k + 1, st[v] + 1);
			}
			return Enumerable.Range(0, max + 1).Max(i => st[i]);
		}
	}
}
