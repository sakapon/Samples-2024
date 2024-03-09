﻿
namespace AlgorithmLib10.SegTrees.SegTrees209
{
	public class Int32RAQTree
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

		public void Clear() => Root = null;

		public long this[int key] => Get(key);

		public void Add(int l, int r, long value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Add(ref Root, l, r, value);

			void Add(ref Node node, int l, int r, long value)
			{
				if (node == null)
				{
					if (l + 1 == r)
					{
						node = new Node { L = l, R = r, Value = value };
						return;
					}
					else
					{
						var f = MaxBit(l ^ (r - 1));
						var nl = l & ~(f | (f - 1));
						node = new Node { L = nl, R = nl + (f << 1) };
					}
				}

				if (node.L == l && r == node.R) { node.Value += value; return; }

				if (!(node.L <= l && r <= node.R))
				{
					var child = node;
					var nl = node.L < l ? node.L : l;
					var f = MaxBit(nl ^ (node.R > r ? node.R - 1 : r - 1));
					nl &= ~(f | (f - 1));
					node = new Node { L = nl, R = nl + (f << 1) };
					(child.L < (nl | f) ? ref node.Left : ref node.Right) = child;
				}

				var nc = (node.L + node.R) >> 1;
				if (l < nc) Add(ref node.Left, l, nc < r ? nc : r, value);
				if (nc < r) Add(ref node.Right, l < nc ? nc : l, r, value);
			}
		}

		public long Get(int key)
		{
			return Get(Root, key);

			long Get(Node node, int key)
			{
				if (node == null) return 0;
				if (key == node.L && key + 1 == node.R) return node.Value;
				if (!(node.L <= key && key < node.R)) return 0;
				var nc = node.L + node.R >> 1;
				return Get(key < nc ? node.Left : node.Right, key) + node.Value;
			}
		}

		public void Add(int key, long value)
		{
			Add(ref Root, key, value);

			void Add(ref Node node, int key, long value)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Value = value };
				}
				else if (key == node.L && key + 1 == node.R)
				{
					node.Value += value;
				}
				else if (node.L <= key && key < node.R)
				{
					var nc = node.L + node.R >> 1;
					Add(ref key < nc ? ref node.Left : ref node.Right, key, value);
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1) };
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

		static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}
	}
}
