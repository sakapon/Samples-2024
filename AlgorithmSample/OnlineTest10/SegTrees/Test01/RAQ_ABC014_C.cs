using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees.Test01
{
	// Test: https://atcoder.jp/contests/abc014/tasks/abc014_3
	class RAQ_ABC014_C
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var ps = Array.ConvertAll(new bool[n], _ => Read2());

			var raq = new RAQTree(1 << 20);
			var u = new bool[1 << 20];

			foreach (var (a, b) in ps)
			{
				raq.Add(a, b + 1, 1);
				u[a] = true;
			}
			return Enumerable.Range(0, u.Length).Where(i => u[i]).Max(i => raq[i]);
		}
	}
}
