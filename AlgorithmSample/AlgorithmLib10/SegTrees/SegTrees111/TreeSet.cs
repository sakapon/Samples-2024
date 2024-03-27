
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public abstract class TreeSetBase
	{
		readonly int n;
		readonly long[] counts;
		public long Count => counts[1];

		protected TreeSetBase(int size)
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

		protected bool AddInternal(int key, long count, long maxCount)
		{
			var i = n | key;
			if (counts[i] + count > maxCount) return false;
			for (; i != 0; i >>= 1) counts[i] += count;
			return true;
		}

		protected bool RemoveInternal(int key, long count)
		{
			var i = n | key;
			if (counts[i] < count) return false;
			for (; i != 0; i >>= 1) counts[i] -= count;
			return true;
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
	}

	public class TreeSet : TreeSetBase
	{
		public TreeSet(int size) : base(size) { }
		public bool Add(int key) => AddInternal(key, 1, 1);
		public bool Remove(int key) => RemoveInternal(key, 1);
		public bool Contains(int key) => GetCount(key) != 0;
	}

	public class TreeMultiSet : TreeSetBase
	{
		public TreeMultiSet(int size) : base(size) { }
		public bool Add(int key, long count = 1) => AddInternal(key, count, long.MaxValue);
		public bool Remove(int key, long count = 1) => RemoveInternal(key, count);
	}
}
