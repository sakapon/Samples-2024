
namespace AlgorithmLib10.SegTrees.SegTrees114
{
	public class Int32RAQTree
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30, MaxIndex = 1 << 30;
		long[] values;
		int[] ln, rn;
		int t;
		int Root;

		public Int32RAQTree(int size = 1 << 22) => Initialize(size);
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

		public void Add(int l, int r, long value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Add(ref Root, MinIndex, MaxIndex, l, r, value);
		}

		void Add(ref int node, int nl, int nr, int l, int r, long value)
		{
			if (node == -1) node = ++t;
			if (nl == l && nr == r) { values[node] += value; return; }
			var nc = nl + nr >> 1;
			if (l < nc) Add(ref ln[node], nl, nc, l, nc < r ? nc : r, value);
			if (nc < r) Add(ref rn[node], nc, nr, l < nc ? nc : l, r, value);
		}

		public long Get(int key)
		{
			var v = 0L;
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == -1) return v;
				v += values[node];
				if (nl + 1 == nr) return v;
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
				if (nl + 1 == nr) { values[node] += value; return; }
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref ln[node]; }
				else { nl = nc; node = ref rn[node]; }
			}
		}
	}
}
