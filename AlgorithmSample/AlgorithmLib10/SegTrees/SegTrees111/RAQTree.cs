
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public class RAQTree
	{
		int n;
		long[] values;

		public RAQTree(int size = 1 << 18) => Initialize(size);
		public void Clear() => Initialize(n);

		void Initialize(int size)
		{
			n = 1;
			while (n < size) n <<= 1;
			values = new long[n << 1];
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
			for (l += n, r += n; l != r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) values[l++] += value;
				if ((r & 1) != 0) values[--r] += value;
			}
		}
	}
}
