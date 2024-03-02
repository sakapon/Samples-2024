
namespace AlgorithmLib10.SegTrees.SegTrees114
{
	public class Int32SplitTree<TValue>
	{
		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30, MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		TValue[] values;
		int[] ln, rn;
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
			ln = new int[size];
			rn = new int[size];
			Array.Fill(ln, -1);
			Array.Fill(rn, -1);
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
				if (ln[node] == -1) return values[node];

				values[ln[node]] = op(values[node], values[ln[node]]);
				values[rn[node]] = op(values[node], values[rn[node]]);
				values[node] = iv;

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

				if (ln[node] == -1)
				{
					ln[node] = ++t;
					values[t] = values[node];
					rn[node] = ++t;
					values[t] = values[node];
				}
				else
				{
					values[ln[node]] = op(values[node], values[ln[node]]);
					values[rn[node]] = op(values[node], values[rn[node]]);
				}
				values[node] = iv;

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
		}

		public void Set(int l, int r, TValue value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Split(Root, MinIndex, MaxIndex, l, r, value);
		}

		void Split(int node, int nl, int nr, int l, int r, TValue value)
		{
			if (nl == l && nr == r)
			{
				values[node] = op(value, values[node]);
				return;
			}

			if (ln[node] == -1)
			{
				ln[node] = ++t;
				values[t] = values[node];
				rn[node] = ++t;
				values[t] = values[node];
			}
			else
			{
				values[ln[node]] = op(values[node], values[ln[node]]);
				values[rn[node]] = op(values[node], values[rn[node]]);
			}
			values[node] = iv;

			var nc = nl + nr >> 1;
			if (l < nc) Split(ln[node], nl, nc, l, nc < r ? nc : r, value);
			if (nc < r) Split(rn[node], nc, nr, l < nc ? nc : l, r, value);
		}
	}
}
