
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public class RAQTree
	{
		readonly int n;
		readonly long[] values;

		public RAQTree(int size)
		{
			n = 1;
			while (n < size) n <<= 1;
			values = new long[n << 1];
			Clear();
		}
		public void Clear()
		{
			Array.Clear(values, 0, values.Length);
		}

		public long this[int key] => Get(key);

		public long Get(int key)
		{
			var v = 0L;
			for (int i = n | key; i != 0; i >>= 1) v += values[i];
			return v;
		}

		public void Add(int key, long value) => values[n | key] += value;
		public void Add(int l, int r, long value)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;
			for (l += n, r += n; l != r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) values[l++] += value;
				if ((r & 1) != 0) values[--r] += value;
			}
		}
	}
}
