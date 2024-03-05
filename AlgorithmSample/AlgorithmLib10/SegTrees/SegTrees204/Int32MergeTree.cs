
namespace AlgorithmLib10.SegTrees.SegTrees204
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
		const int MinIndex = 0, MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		Node Root;
		readonly List<Node> Path = new List<Node>();

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
		}

		TValue Get(Node node, int l, int r)
		{
			if (node == null) return iv;
			if (l <= node.L && node.R <= r) return node.Value;
			var nc = node.L + node.R >> 1;
			var v = l < nc ? Get(node.Left, l, nc < r ? nc : r) : iv;
			return nc < r ? op(v, Get(node.Right, l < nc ? nc : l, r)) : v;
		}

		public TValue Get(int key)
		{
			var node = Root;
			while (true)
			{
				if (node == null) return iv;
				if (!(node.L <= key && key < node.R)) return iv;
				if (key == node.L && key + 1 == node.R) return node.Value;
				var nc = node.L + node.R >> 1;
				node = key < nc ? node.Left : node.Right;
			}
		}

		public void Set(int key, TValue value)
		{
			var node = GetOrAddNode(key);
			node.Value = value;
			for (int i = Path.Count - 2; i >= 0; --i)
			{
				node = Path[i];
				var v = node.Left != null ? node.Left.Value : iv;
				node.Value = node.Right != null ? op(v, node.Right.Value) : v;
			}
		}

		Node GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			while (true)
			{
				if (node == null)
				{
					node = new Node { L = key, R = key + 1 };
					Path.Add(node);
					return node;
				}
				else if (node.L <= key && key < node.R)
				{
					Path.Add(node);
					if (key == node.L && key + 1 == node.R) return node;
					var nc = node.L + node.R >> 1;
					node = ref (key < nc ? ref node.Left : ref node.Right);
				}
				else
				{
					var child = node;
					var f = MaxBit(node.L ^ key);
					var l = key & ~(f | (f - 1));
					node = new Node { L = l, R = l + (f << 1) };
					Path.Add(node);
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
