
namespace AlgorithmLib10.SegTrees.SegTrees103
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

			TValue Get(Node node, int nl, int nr, int l, int r)
			{
				if (node == null) return iv;
				if (nl == l && nr == r) return node.Value;
				var nc = nl + nr >> 1;
				var v = l < nc ? Get(node.Left, nl, nc, l, nc < r ? nc : r) : iv;
				return nc < r ? op(v, Get(node.Right, nc, nr, l < nc ? nc : l, r)) : v;
			}
		}

		public TValue Get(int key)
		{
			return Get(Root, MinIndex, MaxIndex, key);

			TValue Get(Node node, int nl, int nr, int key)
			{
				if (node == null) return iv;
				if (nl + 1 == nr) return node.Value;
				var nc = nl + nr >> 1;
				return key < nc ? Get(node.Left, nl, nc, key) : Get(node.Right, nc, nr, key);
			}
		}

		public void Set(int key, TValue value)
		{
			Set(ref Root, MinIndex, MaxIndex, key, value);

			void Set(ref Node node, int nl, int nr, int key, TValue value)
			{
				node ??= new Node();
				if (nl + 1 == nr) { node.Value = value; return; }
				var nc = nl + nr >> 1;
				if (key < nc) Set(ref node.Left, nl, nc, key, value);
				else Set(ref node.Right, nc, nr, key, value);

				var v = node.Left != null ? node.Left.Value : iv;
				node.Value = node.Right != null ? op(v, node.Right.Value) : v;
			}
		}
	}
}
