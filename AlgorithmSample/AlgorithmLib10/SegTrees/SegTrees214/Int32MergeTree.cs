
namespace AlgorithmLib10.SegTrees.SegTrees214
{
	public class Int32MergeTree<TValue>
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = 0, MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		int[] li, ri;
		int[] ln, rn;
		TValue[] values;
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
			li = new int[size];
			ri = new int[size];
			ln = new int[size];
			rn = new int[size];
			values = new TValue[size];
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

		public TValue Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, l, r);
		}

		TValue Get(int node, int l, int r)
		{
			if (node == -1) return iv;
			if (l <= li[node] && ri[node] <= r) return values[node];
			var nc = li[node] + ri[node] >> 1;
			var v = l < nc ? Get(ln[node], l, nc < r ? nc : r) : iv;
			return nc < r ? op(v, Get(rn[node], l < nc ? nc : l, r)) : v;
		}

		public TValue Get(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == -1) return iv;
				if (!(li[node] <= key && key < ri[node])) return iv;
				if (key == li[node] && key + 1 == ri[node]) return values[node];
				var nc = li[node] + ri[node] >> 1;
				node = key < nc ? ln[node] : rn[node];
			}
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

		int GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == -1)
				{
					node = ++t;
					li[node] = key;
					ri[node] = key + 1;
					Path.Add(node);
					return node;
				}
				else if (li[node] <= key && key < ri[node])
				{
					Path.Add(node);
					if (key == li[node] && key + 1 == ri[node]) return node;
					var nc = li[node] + ri[node] >> 1;
					node = ref (key < nc ? ref ln[node] : ref rn[node]);
				}
				else
				{
					var child = node;
					var f = MaxBit(li[node] ^ key);
					var l = key & ~(f | (f - 1));
					node = ++t;
					li[node] = l;
					ri[node] = l + (f << 1);
					Path.Add(node);
					if (li[child] < (l | f))
					{
						ln[node] = child;
						node = ref rn[node];
					}
					else
					{
						rn[node] = child;
						node = ref ln[node];
					}
				}
			}
		}

		static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}
	}
}
