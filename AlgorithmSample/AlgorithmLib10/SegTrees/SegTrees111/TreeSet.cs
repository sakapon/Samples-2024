
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public abstract class TreeSetBase
	{
		readonly int n;
		readonly long[] counts;
		public long Count => counts[1];

		public TreeSetBase(int size)
		{
			n = 1;
			while (n < size) n <<= 1;
			counts = new long[n << 1];
			Clear();
		}
		public void Clear()
		{
			Array.Clear(counts, 0, counts.Length);
		}

		public long GetCount(int key) => counts[n | key];
		public long GetCount(int l, int r)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;
			var v = 0L;
			for (l += n, r += n; l != r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) v += counts[l++];
				if ((r & 1) != 0) v += counts[--r];
			}
			return v;
		}

		protected bool AddInternal(int key, long count, long maxCount)
		{
			var i = n | key;
			if (counts[i] + count > maxCount) return false;
			for (; i != 0; i >>= 1) counts[i] += count;
			return true;
		}
	}
}
