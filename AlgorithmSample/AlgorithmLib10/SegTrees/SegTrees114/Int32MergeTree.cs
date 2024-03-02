
namespace AlgorithmLib10.SegTrees.SegTrees114
{
	public class Int32MergeTree<TValue>
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30, MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		TValue[] values;
		int[] ln, rn;
		int t;
		int Root;
		readonly List<int> Path = new List<int>();

		public Int32MergeTree(Monoid<TValue> monoid, int size = 1 << 22)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			Initialize(size);
		}
		public void Clear() => Initialize(values.Length);
		void Initialize(int size)
		{
			values = new TValue[size];
			ln = new int[size];
			rn = new int[size];
			Array.Fill(ln, -1);
			Array.Fill(rn, -1);
			t = 0;
		}

		public TValue this[int key]
		{
			get => Get(key);
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int key)
		{
			var node = GetNode(key);
			return node != -1 ? values[node] : iv;
		}

		public TValue Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, MinIndex, MaxIndex, l, r);
		}

		public void Set(int key, TValue value)
		{
			var node = GetOrAddNode(key);
			values[node] = value;
			for (int i = Path.Count - 2; i >= 0; --i)
			{
				node = Path[i];
				var v = ln[node] != -1 ? values[ln[node]] : iv;
				values[node] = rn[node] != -1 ? op(v, values[rn[node]]) : v;
			}
		}

		TValue Get(int node, int nl, int nr, int l, int r)
		{
			if (node == -1) return iv;
			if (nl == l && nr == r) return values[node];
			var nc = nl + nr >> 1;
			var v = l < nc ? Get(ln[node], nl, nc, l, nc < r ? nc : r) : iv;
			return nc < r ? op(v, Get(rn[node], nc, nr, l < nc ? nc : l, r)) : v;
		}

		int GetNode(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == -1) break;
				if (nl + 1 == nr) break;
				var nc = nl + nr >> 1;
				if (key < nc)
				{
					node = ln[node];
					nr = nc;
				}
				else
				{
					node = rn[node];
					nl = nc;
				}
			}
			return node;
		}

		int GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == -1) node = ++t;
				Path.Add(node);
				if (nl + 1 == nr) break;
				var nc = nl + nr >> 1;
				if (key < nc)
				{
					node = ref ln[node];
					nr = nc;
				}
				else
				{
					node = ref rn[node];
					nl = nc;
				}
			}
			return node;
		}
	}
}
