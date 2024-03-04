
namespace AlgorithmLib10.SegTrees.SegTrees204
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
		const int MinIndex = -1 << 30, MaxIndex = 1 << 30;
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
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				node ??= new Node();
				Path.Add(node);
				if (nl + 1 == nr) return node;
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref node.Left; }
				else { nl = nc; node = ref node.Right; }
			}
		}
	}
}
