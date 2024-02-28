using AlgorithmLib10.SegTrees.SegTrees111;

namespace OnlineTest10.SegTrees.Test01
{
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
	class RSQ_Practice2_B
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static void Main()
		{
			var (n, qc) = Read2();
			var a = Read();

			var rsq = new RSQTree(n);
			for (int i = 0; i < n; i++)
			{
				rsq.Add(i, a[i]);
				//rsq[i] = a[i];
			}

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			while (qc-- > 0)
			{
				var q = Read();
				if (q[0] == 0)
					rsq.Add(q[1], q[2]);
				else
					Console.WriteLine(rsq[q[1], q[2]]);
			}
			Console.Out.Flush();
		}
	}
}
