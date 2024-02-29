
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public class SplitTree<TValue>
	{
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		readonly int n;
		readonly TValue[] values;

		public SplitTree(int size, Monoid<TValue> monoid)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			n = 1;
			while (n < size) n <<= 1;
			values = new TValue[n << 1];
			Clear();
		}
		public void Clear()
		{
			Array.Fill(values, iv);
		}

		public TValue this[int key] => Get(key);

		public TValue Get(int key)
		{
			Split(key);
			return values[n | key];
		}

		public void Set(int key, TValue value)
		{
			Split(key);
			values[n | key] = op(value, values[n | key]);
		}

		public void Set(int l, int r, TValue value)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;
			Split(1, 0, n, l, r, value);
		}

		void Split(int key)
		{
			for (int i = 1, f = n >> 1; f != 0; f >>= 1)
			{
				values[i << 1] = op(values[i], values[i << 1]);
				values[(i << 1) | 1] = op(values[i], values[(i << 1) | 1]);
				values[i] = iv;

				i <<= 1;
				if ((key & f) != 0) i |= 1;
			}
		}

		void Split(int i, int nl, int nr, int l, int r, TValue value)
		{
			if (nl == l && nr == r)
			{
				values[i] = op(value, values[i]);
				return;
			}
			values[i << 1] = op(values[i], values[i << 1]);
			values[(i << 1) | 1] = op(values[i], values[(i << 1) | 1]);
			values[i] = iv;

			var nc = nl + nr >> 1;
			if (l < nc) Split(i << 1, nl, nc, l, nc < r ? nc : r, value);
			if (nc < r) Split((i << 1) | 1, nc, nr, l < nc ? nc : l, r, value);
		}
	}
}
