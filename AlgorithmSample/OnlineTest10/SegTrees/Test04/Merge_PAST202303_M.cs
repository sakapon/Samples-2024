using AlgorithmLib10.SegTrees.SegTrees214;

namespace OnlineTest10.SegTrees.Test04
{
	// Test: https://atcoder.jp/contests/past202303-open/tasks/past202303_m
	class Merge_PAST202303_M
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var (n, m) = Read2();
			var a = Read();
			var b = Read();

			var t = (int[])b.Clone();

			var d = new Dictionary<int, PriorityQueue<int, int>>();
			for (int j = 0; j < m; j++)
			{
				if (!d.TryGetValue(b[j], out var q))
					d[b[j]] = q = new PriorityQueue<int, int>();
				q.Enqueue(j, j);
			}

			var st = new Int32MergeTree<int>(Monoid.Int32_Min);
			foreach (var (x, q) in d)
			{
				st[x] = q.Peek();
			}

			for (int i = 0; i < n; i++)
			{
				var j = st[a[i], 1 << 30];
				if (j == int.MaxValue) return $"No\n{i + 1}";

				var x0 = t[j];
				var x = t[j] -= a[i];

				var q = d[x0];
				q.Dequeue();
				st[x0] = q.Count > 0 ? q.Peek() : int.MaxValue;

				if (!d.TryGetValue(x, out q))
					d[x] = q = new PriorityQueue<int, int>();
				q.Enqueue(j, j);
				st[x] = q.Peek();
			}
			return "Yes\n" + string.Join(" ", Enumerable.Range(0, m).Select(j => b[j] - t[j]));
		}
	}
}
