using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc140/tasks/abc140_f
	class MSet_ABC140_F
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		static bool Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var s = Read();

			Array.Sort(s);

			var l = new List<int> { s[^1] };
			var set = new Int32TreeMultiSet();
			foreach (var x in s[..^1]) set.Add(x);

			for (int k = 0; k < n; k++)
			{
				for (int i = 0; i < 1 << k; i++)
				{
					var x = set.RemoveLastLeq(l[i] - 1);
					if (x == int.MinValue) return false;
					l.Add(x);
				}
			}
			return true;
		}
	}
}
