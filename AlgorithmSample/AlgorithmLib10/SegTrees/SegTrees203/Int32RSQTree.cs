﻿
namespace AlgorithmLib10.SegTrees.SegTrees203
{
	public class Int32RSQTree
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
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
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

		public long Get(int key)
		{
			return Get(Root, key);

			long Get(Node node, int key)
			{
				if (node == null) return 0;
				if (key == node.L && key + 1 == node.R) return node.Value;
				if (!(node.L <= key && key < node.R)) return 0;
				var nc = node.L + node.R >> 1;
				return Get(key < nc ? node.Left : node.Right, key);
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
					node.Value += value;
					var nc = node.L + node.R >> 1;
					Add(ref key < nc ? ref node.Left : ref node.Right, key, value);
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1), Value = child.Value + value };
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
