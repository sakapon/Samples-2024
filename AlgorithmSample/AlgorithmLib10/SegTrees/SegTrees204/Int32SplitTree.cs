
namespace AlgorithmLib10.SegTrees.SegTrees204
{
	public class Int32SplitTree<TValue>
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

		public Int32SplitTree(Monoid<TValue> monoid)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			Clear();
		}
		public void Clear() => Root = new Node { Value = iv };

		public TValue this[int key] => Get(key);

		public TValue Get(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				// 子の数は 0 または 2
				if (node.Left == null) return node.Value;

				node.Left.Value = op(node.Value, node.Left.Value);
				node.Right.Value = op(node.Value, node.Right.Value);
				node.Value = iv;

				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = node.Left; }
				else { nl = nc; node = node.Right; }
			}
		}

		public void Set(int key, TValue value)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (nl + 1 == nr) { node.Value = op(value, node.Value); return; }

				if (node.Left == null)
				{
					node.Left = new Node { Value = node.Value };
					node.Right = new Node { Value = node.Value };
				}
				else
				{
					node.Left.Value = op(node.Value, node.Left.Value);
					node.Right.Value = op(node.Value, node.Right.Value);
				}
				node.Value = iv;

				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = node.Left; }
				else { nl = nc; node = node.Right; }
			}
		}

		public void Set(int l, int r, TValue value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Set(Root, MinIndex, MaxIndex, l, r, value);
		}

		void Set(Node node, int nl, int nr, int l, int r, TValue value)
		{
			if (nl == l && nr == r) { node.Value = op(value, node.Value); return; }

			if (node.Left == null)
			{
				node.Left = new Node { Value = node.Value };
				node.Right = new Node { Value = node.Value };
			}
			else
			{
				node.Left.Value = op(node.Value, node.Left.Value);
				node.Right.Value = op(node.Value, node.Right.Value);
			}
			node.Value = iv;

			var nc = nl + nr >> 1;
			if (l < nc) Set(node.Left, nl, nc, l, nc < r ? nc : r, value);
			if (nc < r) Set(node.Right, nc, nr, l < nc ? nc : l, r, value);
		}
	}
}
