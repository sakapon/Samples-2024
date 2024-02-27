﻿
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public class RSQTree
	{
		int n;
		long[] values;

		public RSQTree(int size = 1 << 18) => Initialize(size);
		public void Clear() => Initialize(n);

		void Initialize(int size)
		{
			n = 1;
			while (n < size) n <<= 1;
			values = new long[n << 1];
		}

		public long this[int key]
		{
			get => values[n | key];
			set => Add(key, value - values[n | key]);
		}
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
		{
			var v = 0L;
			for (l += n, r += n; l != r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) v += values[l++];
				if ((r & 1) != 0) v += values[--r];
			}
			return v;
		}

		public void Add(int key, long value)
		{
			for (int i = n | key; i != 0; i >>= 1) values[i] += value;
		}
	}
}
