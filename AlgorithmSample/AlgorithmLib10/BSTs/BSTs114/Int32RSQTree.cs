
namespace AlgorithmLib10.DataTrees.BSTs.BSTs114
{
	public class Int32RSQTree
	{
		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
		long[] values;
		int[] l, r;
		int t;
		int Root;
		readonly List<int> Path = new List<int>();

		public Int32RSQTree(int size = 1 << 22)
		{
			Initialize(size);
		}

		public void Clear()
		{
			Initialize(l.Length);
		}

		void Initialize(int size)
		{
			values = new long[size];
			l = new int[size];
			r = new int[size];
			Array.Fill(l, -1);
			Array.Fill(r, -1);
			t = 0;
		}

		public long this[int key] => this[key, key + 1];
		public long this[int l, int r]
		{
			get
			{
				ScanNode(l, r);
				var v = 0L;
				foreach (var n in Path) v += values[n];
				return v;
			}
		}

		public void Add(int key, long value)
		{
			AddNode(key);
			foreach (var n in Path) values[n] += value;
		}

		void ScanNode(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, -1 << MaxDigit, 1 << MaxDigit, l, r);
		}

		void ScanNode(int node, int nl, int nr, int l, int r)
		{
			if (node == -1) return;
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = (nl + nr) >> 1;
			if (l < nc) ScanNode(this.l[node], nl, nc, l, nc < r ? nc : r);
			if (nc < r) ScanNode(this.r[node], nc, nr, l < nc ? nc : l, r);
		}

		void AddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			var nc = 0;
			for (int d = MaxDigit - 1; d >= -2; --d)
			{
				if (node == -1) node = ++t;
				Path.Add(node);
				if (key < nc)
				{
					node = ref l[node];
					nc -= 1 << d;
				}
				else
				{
					node = ref r[node];
					nc |= 1 << d;
				}
			}
		}
	}
}
