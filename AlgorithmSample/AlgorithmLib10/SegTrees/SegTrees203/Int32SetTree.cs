
namespace AlgorithmLib10.SegTrees.SegTrees203
{
	public class Int32SetTree
	{
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Value = {Value}")]
		public class Node
		{
			public int L, R;
			public Node Left, Right;
			public long Value;
		}

		// [MinIndex, MaxIndex)
		const int MinIndex = 0;
		const int MaxIndex = 1 << 30;
		Node Root;
		public long Count => Root != null ? Root.Value : 0;

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

		public bool Add(int key)
		{
			return Add(ref Root, key);

			bool Add(ref Node node, int key)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Value = 1 };
					return true;
				}
				else if (key == node.L && key + 1 == node.R)
				{
					if (node.Value != 0) return false;
					node.Value = 1;
					return true;
				}
				else if (node.L <= key && key < node.R)
				{
					var nc = node.L + node.R >> 1;
					var b = Add(ref key < nc ? ref node.Left : ref node.Right, key);
					if (b) ++node.Value;
					return b;
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Value = child.Value + 1 };
					if (child.L < (l | f))
					{
						node.Left = child;
						Add(ref node.Right, key);
					}
					else
					{
						node.Right = child;
						Add(ref node.Left, key);
					}
					return true;
				}
			}
		}

		public bool Remove(int key)
		{
			return Remove(ref Root, key);

			bool Remove(ref Node node, int key)
			{
				if (node == null) return false;
				if (key == node.L && key + 1 == node.R)
				{
					if (node.Value == 0) return false;
					node.Value = 0;
					return true;
				}
				if (!(node.L <= key && key < node.R)) return false;
				var nc = node.L + node.R >> 1;
				var b = Remove(ref key < nc ? ref node.Left : ref node.Right, key);
				if (b) --node.Value;
				return b;
			}
		}

		public bool Contains(int key)
		{
			return Get(Root, key);

			bool Get(Node node, int key)
			{
				if (node == null) return false;
				if (key == node.L && key + 1 == node.R) return node.Value != 0;
				if (!(node.L <= key && key < node.R)) return false;
				var nc = node.L + node.R >> 1;
				return Get(key < nc ? node.Left : node.Right, key);
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
				if (l <= node.L && node.R <= r) return node.Value;
				var nc = node.L + node.R >> 1;
				var v = l < nc ? Get(node.Left, l, nc < r ? nc : r) : 0;
				return nc < r ? v + Get(node.Right, l < nc ? nc : l, r) : v;
			}
		}

		public long GetFirstIndexGeq(int key)
		{
			return Get(Root, key);

			long Get(Node node, int key)
			{
				if (node == null) return 0;
				if (key <= node.L) return 0;
				if (node.R <= key) return node.Value;
				var nc = node.L + node.R >> 1;
				return key < nc ? Get(node.Left, key) : Get(node.Right, key) + (node.Left?.Value ?? 0);
			}
		}
		public long GetLastIndexLeq(int key) => GetFirstIndexGeq(key + 1) - 1;

		public long GetIndex(int key)
		{
			return Get(Root, key);

			long Get(Node node, int key)
			{
				if (node == null) return -1;
				if (key == node.L && key + 1 == node.R) return 0;
				if (!(node.L <= key && key < node.R)) return -1;
				var nc = node.L + node.R >> 1;
				var index = Get(key < nc ? node.Left : node.Right, key);
				if (index == -1) return -1;
				if (nc <= key && node.Left != null) index += node.Left.Value;
				return index;
			}
		}

		public int GetAt(long index)
		{
			if (index < 0) return int.MinValue;
			if (index >= Count) return int.MaxValue;
			return Get(Root, index);

			int Get(Node node, long index)
			{
				if (node.Left == null && node.Right == null) return node.L;
				var lc = node.Left?.Value ?? 0;
				return index < lc ? Get(node.Left, index) : Get(node.Right, index - lc);
			}
		}

		public int GetFirst() => GetAt(0);
		public int GetLast() => GetAt(Count - 1);
		public int GetFirstGeq(int key) => GetAt(GetFirstIndexGeq(key));
		public int GetLastLeq(int key) => GetAt(GetLastIndexLeq(key));
	}
}
