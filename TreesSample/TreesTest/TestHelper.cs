namespace TreesTest
{
	public static class TestHelper
	{
		static readonly Random random = new Random();

		public static T[] Shuffle<T>(T[] a)
		{
			var n = a.Length;
			var r = new T[n];
			for (int i = 0; i < n; ++i)
			{
				var j = random.Next(n - i);
				r[i] = a[j];
				a[j] = a[^(i + 1)];
			}
			return r;
		}
		public static int[] Shuffle(int n)
		{
			var a = new int[n];
			for (int i = 0; i < n; ++i) a[i] = i;
			return Shuffle(a);
		}

		#region Trees
		public static (int u, int v)[] CreateTree(int n, bool from1 = true)
		{
			var a = new int[n];
			for (int i = 0; i < n; ++i) a[i] = from1 ? i + 1 : i;
			a = Shuffle(a);

			var r = new (int, int)[n - 1];
			for (int i = 1; i < n; ++i) r[i - 1] = (a[random.Next(i)], a[i]);
			return r;
		}

		public static void WriteTree(int n, bool from1 = true) => WriteTree(n, CreateTree(n, from1));
		public static void WriteTree(int n, (int u, int v)[] es)
		{
			var ess = string.Join("\n", es.Select(e => $"{e.u} {e.v}"));
			Console.Write($"{n} {n - 1}\n{ess}\n");
		}

		// 各辺が任意の方向を持つ木 (arborescence とは異なる)
		public static (int u, int v)[] CreateDirectedTree(int n, bool from1 = true)
		{
			var a = new int[n];
			for (int i = 0; i < n; ++i) a[i] = from1 ? i + 1 : i;
			a = Shuffle(a);

			var r = new (int, int)[n - 1];
			for (int i = 1; i < n; ++i) r[i - 1] = random.Next(2) == 0 ? (a[random.Next(i)], a[i]) : (a[i], a[random.Next(i)]);
			return r;
		}
		#endregion
	}
}
