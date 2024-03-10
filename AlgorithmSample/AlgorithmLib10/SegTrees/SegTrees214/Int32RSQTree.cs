
namespace AlgorithmLib10.SegTrees.SegTrees214
{
	public class Int32RSQTree
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = 0;
		const int MaxIndex = 1 << 30;
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

			var node = Root;
			while (true)
			{
				if (node == -1) return 0;
				if (l <= li[node] && ri[node] <= r) return values[node];
				var nc = li[node] + ri[node] >> 1;
				if (r <= nc) node = ln[node];
				else if (nc <= l) node = rn[node];
				else break;
			}

			var v = 0L;
			var tn = rn[node];
			node = ln[node];
			while (true)
			{
				if (node == -1) break;
				if (l <= li[node]) { v += values[node]; break; }

				if (l < li[node] + ri[node] >> 1)
				{
					if (rn[node] != -1) v += values[rn[node]];
					node = ln[node];
				}
				else
				{
					node = rn[node];
				}
			}

			node = tn;
			while (true)
			{
				if (node == -1) break;
				if (ri[node] <= r) { v += values[node]; break; }

				if (li[node] + ri[node] >> 1 < r)
				{
					if (ln[node] != -1) v += values[ln[node]];
					node = rn[node];
				}
				else
				{
					node = ln[node];
				}
			}
			return v;
		}

		public long Get(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == -1) return 0;
				if (key == li[node] && key + 1 == ri[node]) return values[node];
				if (!(li[node] <= key && key < ri[node])) return 0;
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
				else if (key == li[node] && key + 1 == ri[node])
				{
					values[node] += value;
					return;
				}
				else if (li[node] <= key && key < ri[node])
				{
					values[node] += value;
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
