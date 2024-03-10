
namespace AlgorithmLib10.SegTrees.SegTrees214
{
	public class Int32MergeTree<TValue>
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = 0;
		const int MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		int[] li, ri;
		int[] ln, rn;
		TValue[] values;
		int Root, t;
		readonly Stack<int> Path = new Stack<int>(32);

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
			Root = t = -1;
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

			var node = Root;
			while (true)
			{
				if (node == -1) return iv;
				if (l <= li[node] && ri[node] <= r) return values[node];
				var nc = li[node] + ri[node] >> 1;
				if (r <= nc) node = ln[node];
				else if (nc <= l) node = rn[node];
				else break;
			}

			var (vl, vr) = (iv, iv);
			var tn = rn[node];
			node = ln[node];
			while (true)
			{
				if (node == -1) break;
				if (l <= li[node]) { vl = op(values[node], vl); break; }

				if (l < li[node] + ri[node] >> 1)
				{
					if (rn[node] != -1) vl = op(values[rn[node]], vl);
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
				if (ri[node] <= r) { vr = op(vr, values[node]); break; }

				if (li[node] + ri[node] >> 1 < r)
				{
					if (ln[node] != -1) vr = op(vr, values[ln[node]]);
					node = rn[node];
				}
				else
				{
					node = ln[node];
				}
			}
			return op(vl, vr);
		}

		public TValue Get(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == -1) return iv;
				if (key == li[node] && key + 1 == ri[node]) return values[node];
				if (!(li[node] <= key && key < ri[node])) return iv;
				var nc = li[node] + ri[node] >> 1;
				node = key < nc ? ln[node] : rn[node];
			}
		}

		public void Set(int key, TValue value)
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
					break;
				}
				else if (key == li[node] && key + 1 == ri[node])
				{
					values[node] = value;
					break;
				}
				else if (li[node] <= key && key < ri[node])
				{
					Path.Push(node);
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
					Path.Push(node);
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

			while (Path.TryPop(out var n))
			{
				var v = ln[n] != -1 ? values[ln[n]] : iv;
				values[n] = rn[n] != -1 ? op(v, values[rn[n]]) : v;
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
