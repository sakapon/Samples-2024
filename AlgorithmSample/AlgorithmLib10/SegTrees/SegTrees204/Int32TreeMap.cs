
namespace AlgorithmLib10.SegTrees.SegTrees204
{
	public class Int32TreeMap<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Count = {Count}")]
		public class Node
		{
			public int L, R;
			public Node Left, Right;
			public long Count;
			public TValue Value;
		}

		// [MinIndex, MaxIndex)
		public const int MinIndex = 0;
		public const int MaxIndex = 1 << 30;
		Node Root;
		public long Count => Root != null ? Root.Count : 0;
		public void Clear() => Root = null;

		readonly TValue iv;
		public Int32TreeMap(TValue iv = default)
		{
			this.iv = iv;
		}

		public TValue this[int key]
		{
			get => TryGetValue(key, out var value) ? value : iv;
			set => AddOrSet(key, value, false);
		}

		public bool Add(int key, TValue value) => AddOrSet(key, value, true);
		public bool ContainsKey(int key) => GetNode(key)?.Count == 1;
		public bool TryGetValue(int key, out TValue value)
		{
			var node = GetNode(key);
			if (node != null && node.Count != 0) { value = node.Value; return true; }
			else { value = default; return false; }
		}

		static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}

		bool AddOrSet(int key, TValue value, bool addOnly)
		{
			return Add(ref Root);

			bool Add(ref Node node)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Count = 1, Value = value };
					return true;
				}
				else if (key == node.L && key + 1 == node.R)
				{
					if (addOnly && node.Count != 0) return false;
					node.Count = 1;
					node.Value = value;
					return true;
				}
				else if (node.L <= key && key < node.R)
				{
					var nc = node.L + node.R >> 1;
					var b = Add(ref key < nc ? ref node.Left : ref node.Right);
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

		public bool Remove(int key)
		{
			return Remove(Root);

			bool Remove(Node node)
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
				var b = Remove(key < nc ? node.Left : node.Right);
				if (b) --node.Count;
				return b;
			}
		}

		Node GetNode(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == null) return null;
				if (key == node.L && key + 1 == node.R) return node;
				if (!(node.L <= key && key < node.R)) return null;
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

		public (int key, TValue value) GetAt(long index)
		{
			if (index < 0) return (int.MinValue, default);
			if (index >= Count) return (int.MaxValue, default);
			var node = GetNodeAt(index, false);
			return (node.L, node.Value);
		}

		public (int key, TValue value) RemoveAt(long index)
		{
			if (index < 0) return (int.MinValue, default);
			if (index >= Count) return (int.MaxValue, default);
			var node = GetNodeAt(index, true);
			return (node.L, node.Value);
		}

		public bool SetAt(long index, TValue value)
		{
			if (index < 0) return false;
			if (index >= Count) return false;
			var node = GetNodeAt(index, false);
			node.Value = value;
			return true;
		}

		Node GetNodeAt(long index, bool remove)
		{
			var node = Root;
			while (true)
			{
				if (remove) --node.Count;
				if (node.Left == null && node.Right == null) return node;
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

		public (int key, TValue value) GetFirst() => GetAt(0);
		public (int key, TValue value) GetLast() => GetAt(Count - 1);
		public (int key, TValue value) GetFirstGeq(int key) => GetAt(GetFirstIndexGeq(key));
		public (int key, TValue value) GetLastLeq(int key) => GetAt(GetLastIndexLeq(key));

		public (int key, TValue value) RemoveFirst() => RemoveAt(0);
		public (int key, TValue value) RemoveLast() => RemoveAt(Count - 1);
		public (int key, TValue value) RemoveFirstGeq(int key) => RemoveAt(GetFirstIndexGeq(key));
		public (int key, TValue value) RemoveLastLeq(int key) => RemoveAt(GetLastIndexLeq(key));

		public (int key, TValue value)[] ToArray()
		{
			var r = new (int key, TValue value)[Count];
			var i = -1;
			Get(Root);
			return r;

			void Get(Node node)
			{
				if (node == null) return;
				if (node.Left == null && node.Right == null)
				{
					if (node.Count != 0) r[++i] = (node.L, node.Value);
					return;
				}
				Get(node.Left);
				Get(node.Right);
			}
		}
	}
}
