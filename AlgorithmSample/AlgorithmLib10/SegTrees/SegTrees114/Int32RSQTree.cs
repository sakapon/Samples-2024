
namespace AlgorithmLib10.SegTrees.SegTrees114
{
	public class Int32RSQTree
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30;
		const int MaxIndex = 1 << 30;
		long[] values;
		int[] ln, rn;
		int t;
		int Root;

		public Int32RSQTree(int size = 1 << 22) => Initialize(size);
		public void Clear() => Initialize(values.Length);
		void Initialize(int size)
		{
			values = new long[size];
			ln = new int[size];
			rn = new int[size];
			Array.Fill(ln, -1);
			Array.Fill(rn, -1);
			t = 0;
		}

		public long this[int key] => Get(key);
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, MinIndex, MaxIndex, l, r);
		}

		long Get(int node, int nl, int nr, int l, int r)
		{
			if (node == -1) return 0;
			if (nl == l && nr == r) return values[node];
			var nc = nl + nr >> 1;
			var v = l < nc ? Get(ln[node], nl, nc, l, nc < r ? nc : r) : 0;
			return nc < r ? v + Get(rn[node], nc, nr, l < nc ? nc : l, r) : v;
		}

		public long Get(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == -1) return 0;
				if (nl + 1 == nr) return values[node];
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ln[node]; }
				else { nl = nc; node = rn[node]; }
			}
		}

		public void Add(int key, long value)
		{
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == -1) node = ++t;
				values[node] += value;
				if (nl + 1 == nr) return;
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref ln[node]; }
				else { nl = nc; node = ref rn[node]; }
			}
		}
	}
}
