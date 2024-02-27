
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public class MergeTree<TValue>
	{
		readonly int n;
		TValue[] values;
		readonly Func<TValue, TValue, TValue> merge;
		readonly TValue iv;

		public MergeTree(Monoid<TValue> monoid, int size = 1 << 18)
		{
			(merge, iv) = (monoid.Op, monoid.Id);
			n = 1;
			while (n < size) n <<= 1;
			Clear();
		}
		public void Clear()
		{
			values = new TValue[n << 1];
		}

		public TValue this[int key]
		{
			get => values[n | key];
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int l, int r)
		{
			var v = iv;
			for (l += n, r += n; l != r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) v = merge(v, values[l++]);
				if ((r & 1) != 0) v = merge(v, values[--r]);
			}
			return v;
		}

		public void Set(int key, TValue value)
		{
			var i = n | key;
			values[i] = value;
			for (i >>= 1; i != 0; i >>= 1) values[i] = merge(values[i << 1], values[(i << 1) + 1]);
		}
	}
}
