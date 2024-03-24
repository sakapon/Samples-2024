using AlgorithmLib10.SegTrees.SegTrees204;

namespace OnlineTest10.SegTrees.SetTest04
{
	// Test: https://atcoder.jp/contests/abc228/tasks/abc228_d
	class Set_ABC228_D
	{
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
		static void Main()
		{
			var qc = int.Parse(Console.ReadLine());

			const int n = 1 << 20;
			var a = new long[n];
			Array.Fill(a, -1);

			var set = new Int32TreeSet();
			for (int i = 0; i < n; i++)
				set.Add(i);

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			while (qc-- > 0)
			{
				var (t, x) = Read2L();
				var xi = (int)(x % n);

				if (t == 1)
				{
					xi = set.RemoveFirstGeq(xi);
					if (xi == int.MaxValue) xi = set.RemoveFirst();
					a[xi] = x;
				}
				else
				{
					Console.WriteLine(a[xi]);
				}
			}
			Console.Out.Flush();
		}
	}
}
