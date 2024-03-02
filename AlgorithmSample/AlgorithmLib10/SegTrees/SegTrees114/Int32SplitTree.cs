
namespace AlgorithmLib10.SegTrees.SegTrees114
{
	public class Int32SplitTree<TValue>
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30, MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		TValue[] values;
		int[] l, r;
		int t;
		readonly int Root;

		public Int32SplitTree(Monoid<TValue> monoid, int size = 1 << 22)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			Initialize(size);
		}
		public void Clear() => Initialize(values.Length);
		void Initialize(int size)
		{
			values = new TValue[size];
			l = new int[size];
			r = new int[size];
			Array.Fill(l, -1);
			Array.Fill(r, -1);
			t = 0;
		}

		public TValue this[int key] => Get(key);

		public TValue Get(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				// 子の数は 0 または 2
				if (l[node] == -1) return values[node];

				values[l[node]] = op(values[node], values[l[node]]);
				values[r[node]] = op(values[node], values[r[node]]);
				values[node] = iv;

				var nc = nl + nr >> 1;
				if (key < nc)
				{
					node = l[node];
					nr = nc;
				}
				else
				{
					node = r[node];
					nl = nc;
				}
			}
		}

		public void Set(int key, TValue value)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (nl + 1 == nr)
				{
					values[node] = op(value, values[node]);
					return;
				}

				if (l[node] == -1)
				{
					l[node] = ++t;
					values[t] = values[node];
					r[node] = ++t;
					values[t] = values[node];
				}
				else
				{
					values[l[node]] = op(values[node], values[l[node]]);
					values[r[node]] = op(values[node], values[r[node]]);
				}
				values[node] = iv;

				var nc = nl + nr >> 1;
				if (key < nc)
				{
					node = l[node];
					nr = nc;
				}
				else
				{
					node = r[node];
					nl = nc;
				}
			}
		}

		public void Set(int li, int ri, TValue value)
		{
			if (li < MinIndex) li = MinIndex;
			if (ri > MaxIndex) ri = MaxIndex;
			Split(Root, MinIndex, MaxIndex, li, ri, value);
		}

		void Split(int node, int nl, int nr, int li, int ri, TValue value)
		{
			if (nl == li && nr == ri)
			{
				values[node] = op(value, values[node]);
				return;
			}

			if (l[node] == -1)
			{
				l[node] = ++t;
				values[t] = values[node];
				r[node] = ++t;
				values[t] = values[node];
			}
			else
			{
				values[l[node]] = op(values[node], values[l[node]]);
				values[r[node]] = op(values[node], values[r[node]]);
			}
			values[node] = iv;

			var nc = nl + nr >> 1;
			if (li < nc) Split(l[node], nl, nc, li, nc < ri ? nc : ri, value);
			if (nc < ri) Split(r[node], nc, nr, li < nc ? nc : li, ri, value);
		}
	}
}
