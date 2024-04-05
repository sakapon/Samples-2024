
namespace AlgorithmLib10.SegTrees.SegTrees304
{
	public abstract class Int32TreeSetBase
	{
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Count = {Count}")]
		public class Node
		{
			public int L, R;
			public Node Left, Right, Parent;
			public long Count;
		}

		// [MinIndex, MaxIndex)
		public const int MinIndex = 0;
		public const int MaxIndex = 1 << 30;
		protected Dictionary<int, Node> Leaves = new Dictionary<int, Node>();
		protected Node Root;
		public long Count => Root != null ? Root.Count : 0;

		public void Clear() => Root = null;

		static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}

		protected bool AddInternal(int key, long count, long maxCount)
		{
			if (count == 0) return true;
			return Add(ref Root, null);

			bool Add(ref Node node, Node p)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Count = count, Parent = p };
					Leaves[key] = node;
					return true;
				}
				else if (key == node.L && key + 1 == node.R)
				{
					if (node.Count + count > maxCount) return false;
					node.Count += count;
					return true;
				}
				else if (node.L <= key && key < node.R)
				{
					var nc = node.L + node.R >> 1;
					var b = Add(ref key < nc ? ref node.Left : ref node.Right, node);
					if (b) node.Count += count;
					return b;
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Count = child.Count + count, Parent = p };
					child.Parent = node;
					if (child.L < (l | f))
					{
						node.Left = child;
						Add(ref node.Right, node);
					}
					else
					{
						node.Right = child;
						Add(ref node.Left, node);
					}
					return true;
				}
			}
		}

		protected bool RemoveInternal(int key, long count)
		{
			if (count == 0) return true;
			if (!Leaves.TryGetValue(key, out var node)) return false;
			if (node.Count < count) return false;
			for (; node != null; node = node.Parent) node.Count -= count;
			return true;
		}

		public long GetCount(int key) => Leaves.TryGetValue(key, out var node) ? node.Count : 0;

		public long GetCount(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, l, r);

			long Get(Node node, int l, int r)
			{
				if (node == null) return 0;
				if (l <= node.L && node.R <= r) return node.Count;
				var nc = node.L + node.R >> 1;
				var v = l < nc ? Get(node.Left, l, nc < r ? nc : r) : 0;
				return nc < r ? v + Get(node.Right, l < nc ? nc : l, r) : v;
			}
		}

		public long GetFirstIndexGeq(int key)
		{
			var node = Root;
			var index = 0L;
			while (true)
			{
				if (node == null) return index;
				if (key <= node.L) return index;
				if (node.R <= key) return index + node.Count;
				var nc = node.L + node.R >> 1;
				if (key < nc)
				{
					node = node.Left;
				}
				else
				{
					if (node.Left != null) index += node.Left.Count;
					node = node.Right;
				}
			}
		}
		public long GetLastIndexLeq(int key) => GetFirstIndexGeq(key + 1) - 1;

		public long GetIndex(int key)
		{
			if (!Leaves.TryGetValue(key, out var node)) return -1;
			if (node.Count == 0) return -1;
			var index = 0L;
			for (var p = node.Parent; p != null; (node, p) = (p, p.Parent))
				if (p.Right == node && p.Left != null) index += p.Left.Count;
			return index;
		}

		public int GetAt(long index) => GetAt(index, false);
		public int RemoveAt(long index) => GetAt(index, true);

		int GetAt(long index, bool remove)
		{
			if (index < 0) return int.MinValue;
			if (index >= Count) return int.MaxValue;

			var node = Root;
			while (true)
			{
				if (remove) --node.Count;
				if (node.Left == null && node.Right == null) return node.L;
				var lc = node.Left?.Count ?? 0;
				if (index < lc)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
					index -= lc;
				}
			}
		}

		public int GetFirst() => GetAt(0);
		public int GetLast() => GetAt(Count - 1);
		public int GetFirstGeq(int key) => GetAt(GetFirstIndexGeq(key));
		public int GetLastLeq(int key) => GetAt(GetLastIndexLeq(key));

		public int RemoveFirst() => RemoveAt(0);
		public int RemoveLast() => RemoveAt(Count - 1);
		public int RemoveFirstGeq(int key) => RemoveAt(GetFirstIndexGeq(key));
		public int RemoveLastLeq(int key) => RemoveAt(GetLastIndexLeq(key));

		protected static Node GetFirstLeaf(Node node)
		{
			if (node == null) return null;
			while (node.Left != null) node = node.Left;
			return node;
		}

		protected static Node GetNextLeaf(Node node)
		{
			for (var p = node.Parent; ; (node, p) = (p, p.Parent))
			{
				if (p == null) return null;
				if (p.Left == node) { node = p.Right; break; }
			}
			return GetFirstLeaf(node);
		}

		public int[] ToArray()
		{
			var r = new int[Count];
			var i = -1;
			for (var node = GetFirstLeaf(Root); node != null; node = GetNextLeaf(node))
			{
				var c = node.Count;
				while (c-- > 0) r[++i] = node.L;
			}
			return r;
		}

		public int[] ToArray(int l, int r)
		{
			var a = new List<int>();
			l = GetFirstGeq(l);
			for (var node = Leaves[l]; node != null && node.L < r; node = GetNextLeaf(node))
			{
				var c = node.Count;
				while (c-- > 0) a.Add(node.L);
			}
			return a.ToArray();
		}
	}

	public class Int32TreeSet : Int32TreeSetBase
	{
		public bool Add(int key) => AddInternal(key, 1, 1);
		public bool Remove(int key) => RemoveInternal(key, 1);
		public bool Contains(int key) => GetCount(key) != 0;
	}

	public class Int32TreeMultiSet : Int32TreeSetBase
	{
		public bool Add(int key, long count = 1) => AddInternal(key, count, long.MaxValue);
		public bool Remove(int key, long count = 1) => RemoveInternal(key, count);

		public (int key, long count)[] ToKeyCountArray()
		{
			var r = new List<(int, long)>();
			for (var node = GetFirstLeaf(Root); node != null; node = GetNextLeaf(node))
				if (node.Count != 0) r.Add((node.L, node.Count));
			return r.ToArray();
		}
	}
}
