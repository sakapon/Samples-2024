
namespace AlgorithmLib10.SegTrees.SegTrees204
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

			var node = Root;
			while (true)
			{
				if (node == null) return 0;
				if (l <= node.L && node.R <= r) return node.Value;
				var nc = node.L + node.R >> 1;
				if (r <= nc) node = node.Left;
				else if (nc <= l) node = node.Right;
				else break;
			}

			var v = 0L;
			var rn = node.Right;
			node = node.Left;
			while (true)
			{
				if (node == null) break;
				if (l <= node.L) { v += node.Value; break; }

				if (l < node.L + node.R >> 1)
				{
					if (node.Right != null) v += node.Right.Value;
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}

			node = rn;
			while (true)
			{
				if (node == null) break;
				if (node.R <= r) { v += node.Value; break; }

				if (node.L + node.R >> 1 < r)
				{
					if (node.Left != null) v += node.Left.Value;
					node = node.Right;
				}
				else
				{
					node = node.Left;
				}
			}
			return v;
		}

		public long Get(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == null) return 0;
				if (key == node.L && key + 1 == node.R) return node.Value;
				if (!(node.L <= key && key < node.R)) return 0;
				var nc = node.L + node.R >> 1;
				node = key < nc ? node.Left : node.Right;
			}
		}

		public void Add(int key, long value)
		{
			ref var node = ref Root;
			while (true)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Value = value };
					return;
				}
				else if (key == node.L && key + 1 == node.R)
				{
					node.Value += value;
					return;
				}
				else if (node.L <= key && key < node.R)
				{
					node.Value += value;
					var nc = node.L + node.R >> 1;
					node = ref (key < nc ? ref node.Left : ref node.Right);
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
						node = ref node.Right;
					}
					else
					{
						node.Right = child;
						node = ref node.Left;
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
