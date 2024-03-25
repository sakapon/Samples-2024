
namespace AlgorithmLib10.SegTrees.SegTrees204
{
	public class Int32TreeMap
	{
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Count = {Count}")]
		public class Node
		{
			public int L, R;
			public Node Left, Right;
			public long Count;
		}

		// [MinIndex, MaxIndex)
		public const int MinIndex = 0;
		public const int MaxIndex = 1 << 30;
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
			return Add(ref Root);

			bool Add(ref Node node)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Count = count };
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
					var b = Add(ref key < nc ? ref node.Left : ref node.Right);
					if (b) node.Count += count;
					return b;
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Count = child.Count + count };
					if (child.L < (l | f))
					{
						node.Left = child;
						Add(ref node.Right);
					}
					else
					{
						node.Right = child;
						Add(ref node.Left);
					}
					return true;
				}
			}
		}

		protected bool RemoveInternal(int key, long count)
		{
			if (count == 0) return true;
			return Remove(Root);

			bool Remove(Node node)
			{
				if (node == null) return false;
				if (key == node.L && key + 1 == node.R)
				{
					if (node.Count < count) return false;
					node.Count -= count;
					return true;
				}
				if (!(node.L <= key && key < node.R)) return false;
				var nc = node.L + node.R >> 1;
				var b = Remove(key < nc ? node.Left : node.Right);
				if (b) node.Count -= count;
				return b;
			}
		}

		public long GetCount(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == null) return 0;
				if (key == node.L && key + 1 == node.R) return node.Count;
				if (!(node.L <= key && key < node.R)) return 0;
				var nc = node.L + node.R >> 1;
				node = key < nc ? node.Left : node.Right;
			}
		}

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
			var node = Root;
			var index = 0L;
			while (true)
			{
				if (node == null) return -1;
				if (key == node.L && key + 1 == node.R) return index;
				if (!(node.L <= key && key < node.R)) return -1;
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
	}
}
