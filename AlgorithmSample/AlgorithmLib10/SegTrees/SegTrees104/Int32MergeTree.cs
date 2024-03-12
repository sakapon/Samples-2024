
namespace AlgorithmLib10.SegTrees.SegTrees104
{
	public class Int32MergeTree<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public TValue Value;
			public Node Left, Right;
		}

		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30;
		const int MaxIndex = 1 << 30;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		Node Root;
		readonly Stack<Node> Path = new Stack<Node>(32);

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
			return Get(Root, MinIndex, MaxIndex, l, r);
		}

		TValue Get(Node node, int nl, int nr, int l, int r)
		{
			if (node == null) return iv;
			if (nl == l && nr == r) return node.Value;
			var nc = nl + nr >> 1;
			var v = l < nc ? Get(node.Left, nl, nc, l, nc < r ? nc : r) : iv;
			return nc < r ? op(v, Get(node.Right, nc, nr, l < nc ? nc : l, r)) : v;
		}

		public TValue Get(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == null) return iv;
				if (nl + 1 == nr) return node.Value;
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = node.Left; }
				else { nl = nc; node = node.Right; }
			}
		}

		public void Set(int key, TValue value)
		{
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				node ??= new Node();
				if (nl + 1 == nr) { node.Value = value; break; }
				Path.Push(node);
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref node.Left; }
				else { nl = nc; node = ref node.Right; }
			}

			while (Path.TryPop(out var n))
			{
				var v = n.Left != null ? n.Left.Value : iv;
				n.Value = n.Right != null ? op(v, n.Right.Value) : v;
			}
		}
	}
}
