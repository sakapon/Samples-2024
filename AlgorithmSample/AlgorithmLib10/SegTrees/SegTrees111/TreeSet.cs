
namespace AlgorithmLib10.SegTrees.SegTrees111
{
	public abstract class TreeSetBase
	{
		protected readonly int n;
		protected readonly long[] counts;
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

		public long GetFirstIndexGeq(int key)
		{
			var i = n | key;
			var index = 0L;
			for (; i != 1; i >>= 1)
				if ((i & 1) != 0) index += counts[i & ~1];
			return index;
		}
		public long GetLastIndexLeq(int key) => GetFirstIndexGeq(key + 1) - 1;

		public long GetIndex(int key)
		{
			var i = n | key;
			if (counts[i] == 0) return -1;
			var index = 0L;
			for (; i != 1; i >>= 1)
				if ((i & 1) != 0) index += counts[i & ~1];
			return index;
		}

		int GetAt(long index, bool remove)
		{
			if (index < 0) return int.MinValue;
			if (index >= Count) return int.MaxValue;

			var i = 1;
			while (true)
			{
				if (remove) --counts[i];
				if (i >= n) return i & ~n;
				if (index >= counts[i <<= 1])
				{
					index -= counts[i];
					i |= 1;
				}
			}
		}

		public int GetAt(long index) => GetAt(index, false);
		public int RemoveAt(long index) => GetAt(index, true);

		public int GetFirst() => GetAt(0);
		public int GetLast() => GetAt(Count - 1);
		public int GetFirstGeq(int key) => GetAt(GetFirstIndexGeq(key));
		public int GetLastLeq(int key) => GetAt(GetLastIndexLeq(key));

		public int RemoveFirst() => RemoveAt(0);
		public int RemoveLast() => RemoveAt(Count - 1);
		public int RemoveFirstGeq(int key) => RemoveAt(GetFirstIndexGeq(key));
		public int RemoveLastLeq(int key) => RemoveAt(GetLastIndexLeq(key));

		public int[] ToArray() => ToArray(0, n);
		public int[] ToArray(int l, int r)
		{
			var a = new int[Count];
			var i = -1;
			for (int key = l; key < r; ++key)
			{
				var c = counts[n | key];
				while (c-- > 0) a[++i] = key;
			}
			return a;
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

		public (int key, long count)[] ToKeyCountArray() => ToKeyCountArray(0, n);
		public (int key, long count)[] ToKeyCountArray(int l, int r)
		{
			var a = new List<(int, long)>();
			for (int key = l; key < r; ++key)
				if (counts[n | key] != 0) a.Add((key, counts[n | key]));
			return a.ToArray();
		}
	}
}
