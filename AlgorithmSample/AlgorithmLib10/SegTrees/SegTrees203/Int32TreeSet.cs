
namespace AlgorithmLib10.SegTrees.SegTrees203
{
	public abstract class Int32TreeSetBase
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

		protected static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}

		public long GetCount(int key)
		{
			return Get(Root, key);

			long Get(Node node, int key)
			{
				if (node == null) return 0;
				if (key == node.L && key + 1 == node.R) return node.Count;
				if (!(node.L <= key && key < node.R)) return 0;
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
				if (l <= node.L && node.R <= r) return node.Count;
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
				if (node.R <= key) return node.Count;
				var nc = node.L + node.R >> 1;
				return key < nc ? Get(node.Left, key) : Get(node.Right, key) + (node.Left?.Count ?? 0);
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
				if (nc <= key && node.Left != null) index += node.Left.Count;
				return index;
			}
		}

		public int GetAt(long index)
		{
			if (index < 0) return int.MinValue;
			if (index >= Count) return int.MaxValue;
			return GetAt(Root, index, false);
		}

		public int RemoveAt(long index)
		{
			if (index < 0) return int.MinValue;
			if (index >= Count) return int.MaxValue;
			return GetAt(Root, index, true);
		}

		int GetAt(Node node, long index, bool remove)
		{
			if (remove) --node.Count;
			if (node.Left == null && node.Right == null) return node.L;
			var lc = node.Left?.Count ?? 0;
			return index < lc ? GetAt(node.Left, index, remove) : GetAt(node.Right, index - lc, remove);
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

	public class Int32TreeSet : Int32TreeSetBase
	{
		public bool Add(int key)
		{
			return Add(ref Root, key);

			bool Add(ref Node node, int key)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Count = 1 };
					return true;
				}
				else if (key == node.L && key + 1 == node.R)
				{
					if (node.Count != 0) return false;
					node.Count = 1;
					return true;
				}
				else if (node.L <= key && key < node.R)
				{
					var nc = node.L + node.R >> 1;
					var b = Add(ref key < nc ? ref node.Left : ref node.Right, key);
					if (b) ++node.Count;
					return b;
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Count = child.Count + 1 };
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
					if (node.Count == 0) return false;
					node.Count = 0;
					return true;
				}
				if (!(node.L <= key && key < node.R)) return false;
				var nc = node.L + node.R >> 1;
				var b = Remove(ref key < nc ? ref node.Left : ref node.Right, key);
				if (b) --node.Count;
				return b;
			}
		}

		public bool Contains(int key) => GetCount(key) != 0;

		public int[] ToArray()
		{
			var r = new int[Count];
			var i = -1;
			Get(Root);
			return r;

			void Get(Node node)
			{
				if (node == null) return;
				if (node.Left == null && node.Right == null)
				{
					if (node.Count != 0) r[++i] = node.L;
					return;
				}
				Get(node.Left);
				Get(node.Right);
			}
		}
	}

	public class Int32TreeMultiSet : Int32TreeSetBase
	{
		public void Add(int key, long value = 1)
		{
			Add(ref Root, key, value);

			void Add(ref Node node, int key, long value)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Count = value };
				}
				else if (key == node.L && key + 1 == node.R)
				{
					node.Count += value;
				}
				else if (node.L <= key && key < node.R)
				{
					node.Count += value;
					var nc = node.L + node.R >> 1;
					Add(ref key < nc ? ref node.Left : ref node.Right, key, value);
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Count = child.Count + value };
					if (child.L < (l | f))
					{
						node.Left = child;
						Add(ref node.Right, key, value);
					}
					else
					{
						node.Right = child;
						Add(ref node.Left, key, value);
					}
				}
			}
		}

		public int[] ToArray()
		{
			var r = new int[Count];
			var i = -1;
			Get(Root);
			return r;

			void Get(Node node)
			{
				if (node == null) return;
				if (node.Left == null && node.Right == null)
				{
					var c = node.Count;
					while (c-- > 0) r[++i] = node.L;
					return;
				}
				Get(node.Left);
				Get(node.Right);
			}
		}

		public (int key, long count)[] ToKeyCountArray()
		{
			var r = new List<(int, long)>();
			Get(Root);
			return r.ToArray();

			void Get(Node node)
			{
				if (node == null) return;
				if (node.Left == null && node.Right == null)
				{
					if (node.Count != 0) r.Add((node.L, node.Count));
					return;
				}
				Get(node.Left);
				Get(node.Right);
			}
		}
	}
}
