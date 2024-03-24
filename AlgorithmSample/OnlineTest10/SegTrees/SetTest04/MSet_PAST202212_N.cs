using AlgorithmLib10.SegTrees.SegTrees203;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/past202212-open/tasks/past202212_n
	class MSet_PAST202212_N
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var a = Read();
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var moq = GetMoQueries(n, qs);
			var vs = new long[qc];
			var v = 0L;
			var (tl, tr) = (1, 1);

			var set = new Int32TreeMultiSet();
			set.Add(a[0]);

			void Incr(int i)
			{
				var x = a[i - 1];
				set.Add(x);
				var si = set.GetIndex(x);
				long d = set.GetAt(si - 1);
				long u = set.GetAt(si + 1);

				if (d == int.MinValue)
				{
					v += (u - x) * (u - x);
				}
				else if (u == int.MaxValue)
				{
					v += (d - x) * (d - x);
				}
				else
				{
					v += (u - x) * (u - x);
					v += (d - x) * (d - x);
					v -= (u - d) * (u - d);
				}
			}
			void Decr(int i)
			{
				var x = a[i - 1];
				var si = set.GetIndex(x);
				long d = set.GetAt(si - 1);
				long u = set.GetAt(si + 1);
				set.Remove(x);

				if (d == int.MinValue)
				{
					v -= (u - x) * (u - x);
				}
				else if (u == int.MaxValue)
				{
					v -= (d - x) * (d - x);
				}
				else
				{
					v -= (u - x) * (u - x);
					v -= (d - x) * (d - x);
					v += (u - d) * (u - d);
				}
			}

			foreach (var qi in moq)
			{
				var (l, r) = qs[qi];

				while (tr < r) Incr(++tr);
				while (l < tl) Incr(--tl);
				while (r < tr) Decr(tr--);
				while (tl < l) Decr(tl++);

				vs[qi] = v;
			}
			return string.Join("\n", vs);
		}

		// クエリ ID を訪問順に返します。
		public static int[] GetMoQueries(int n, (int l, int r)[] qs)
		{
			var qc = qs.Length;

			// ブロック数
			var bc = (int)Math.Sqrt(qc);
			// ブロック長
			var bl = n / bc + 1;

			var qi_map = Array.ConvertAll(new bool[bc], _ => new List<int>());
			var r_map = Array.ConvertAll(new bool[bc], _ => new List<int>());
			for (int qi = 0; qi < qc; qi++)
			{
				var (l, r) = qs[qi];
				l /= bl;
				qi_map[l].Add(qi);
				r_map[l].Add(r);
			}

			var path = new List<int>();
			for (int bi = 0; bi < bc; bi++)
			{
				var qis = qi_map[bi].ToArray();
				var rs = r_map[bi].ToArray();
				Array.Sort(rs, qis);
				path.AddRange(qis);
			}
			return path.ToArray();
		}
	}
}
