
namespace AlgorithmLib10.SegTrees.SegTrees203
{
	public class Int32MergeTree<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"[{L}, {R}), Value = {Value}")]
		public class Node
		{
			public int L, R;
			public Node Left, Right;
			public TValue Value;
		}

		// [MinIndex, MaxIndex)
		const int MinIndex = 0;
		const int MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		Node Root;

		public Int32MergeTree(Monoid<TValue> monoid) => (op, iv) = (monoid.Op, monoid.Id);
		public void Clear() => Root = null;

		public TValue this[int key]
		{
			get => Get(key);
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, l, r);

			TValue Get(Node node, int l, int r)
			{
				if (node == null) return iv;
				if (l <= node.L && node.R <= r) return node.Value;
				var nc = node.L + node.R >> 1;
				var v = l < nc ? Get(node.Left, l, nc < r ? nc : r) : iv;
				return nc < r ? op(v, Get(node.Right, l < nc ? nc : l, r)) : v;
			}
		}

		public TValue Get(int key)
		{
			return Get(Root, key);

			TValue Get(Node node, int key)
			{
				if (node == null) return iv;
				if (key == node.L && key + 1 == node.R) return node.Value;
				if (!(node.L <= key && key < node.R)) return iv;
				var nc = node.L + node.R >> 1;
				return Get(key < nc ? node.Left : node.Right, key);
			}
		}

		public void Set(int key, TValue value)
		{
			Set(ref Root, key, value);

			void Set(ref Node node, int key, TValue value)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1, Value = value };
					return;
				}
				else if (key == node.L && key + 1 == node.R)
				{
					node.Value = value;
					return;
				}
				else if (node.L <= key && key < node.R)
				{
					var nc = node.L + node.R >> 1;
					Set(ref key < nc ? ref node.Left : ref node.Right, key, value);
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
						Set(ref node.Right, key, value);
					}
					else
					{
						node.Right = child;
						Set(ref node.Left, key, value);
					}
				}

				var v = node.Left != null ? node.Left.Value : iv;
				node.Value = node.Right != null ? op(v, node.Right.Value) : v;
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
