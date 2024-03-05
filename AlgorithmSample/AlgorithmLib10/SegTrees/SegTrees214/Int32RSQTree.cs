
namespace AlgorithmLib10.SegTrees.SegTrees214
{
	public class Int32RSQTree
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = 0, MaxIndex = 1 << 30;
		int[] li, ri;
		int[] ln, rn;
		long[] values;
		int Root, t;

		public Int32RSQTree(int size = 1 << 22) => Initialize(size);
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
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, l, r);
		}

		long Get(int node, int l, int r)
		{
			if (node == -1) return 0;
			if (l <= li[node] && ri[node] <= r) return values[node];
			var nc = li[node] + ri[node] >> 1;
			var v = l < nc ? Get(ln[node], l, nc < r ? nc : r) : 0;
			return nc < r ? v + Get(rn[node], l < nc ? nc : l, r) : v;
		}

		public long Get(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == -1) return 0;
				if (!(li[node] <= key && key < ri[node])) return 0;
				if (key == li[node] && key + 1 == ri[node]) return values[node];
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
					values[node] += value;
					if (key == li[node] && key + 1 == ri[node]) return;
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
					values[node] = values[child] + value;
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
