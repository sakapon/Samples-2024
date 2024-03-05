
namespace AlgorithmLib10.SegTrees.SegTrees214
{
	public class Int32RAQTree
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = 0, MaxIndex = 1 << 30;
		int[] li, ri;
		int[] ln, rn;
		long[] values;
		int Root, t;

		public Int32RAQTree(int size = 1 << 22) => Initialize(size);
		public void Clear() => Initialize(values.Length);
		void Initialize(int size)
		{
			li = new int[size];
			ri = new int[size];
			ln = new int[size];
			rn = new int[size];
			values = new long[size];

			Array.Fill(ln, -1);
			Array.Fill(rn, -1);
			Root = t = -1;
		}

		public long this[int key] => Get(key);

		public void Add(int l, int r, long value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Add(ref Root, l, r, value);
		}

		void Add(ref int node, int l, int r, long value)
		{
			if (node == -1)
			{
				if (l + 1 == r)
				{
					node = ++t;
					li[node] = l;
					ri[node] = r;
					values[node] = value;
					return;
				}
				else
				{
					var f = MaxBit(l ^ (r - 1));
					var nl = l & ~(f | (f - 1));
					node = ++t;
					li[node] = nl;
					ri[node] = nl + (f << 1);
				}
			}

			if (li[node] == l && r == ri[node]) { values[node] += value; return; }

			if (!(li[node] <= l && r <= ri[node]))
			{
				var child = node;
				var nl = li[node] < l ? li[node] : l;
				var f = MaxBit(nl ^ (ri[node] > r ? ri[node] - 1 : r - 1));
				nl &= ~(f | (f - 1));
				node = ++t;
				li[node] = nl;
				ri[node] = nl + (f << 1);
				(li[child] < (nl | f) ? ref ln[node] : ref rn[node]) = child;
			}

			var nc = li[node] + ri[node] >> 1;
			if (l < nc) Add(ref ln[node], l, nc < r ? nc : r, value);
			if (nc < r) Add(ref rn[node], l < nc ? nc : l, r, value);
		}

		public long Get(int key)
		{
			var v = 0L;
			var node = Root;
			while (true)
			{
				if (node == -1) return v;
				if (!(li[node] <= key && key < ri[node])) return v;
				v += values[node];
				if (key == li[node] && key + 1 == ri[node]) return v;
				var nc = li[node] + ri[node] >> 1;
				node = key < nc ? ln[node] : rn[node];
			}
		}

		public void Add(int key, long value)
		{
			ref var node = ref Root;
			while (true)
			{
				if (node == -1)
				{
					node = ++t;
					li[node] = key;
					ri[node] = key + 1;
					values[node] = value;
					return;
				}
				else if (li[node] <= key && key < ri[node])
				{
					if (key == li[node] && key + 1 == ri[node]) { values[node] += value; return; }
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
