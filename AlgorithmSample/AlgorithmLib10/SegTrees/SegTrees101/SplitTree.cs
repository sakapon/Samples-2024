
namespace AlgorithmLib10.SegTrees.SegTrees101
{
	public class SplitTree<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public TValue Value;
			public Node Left, Right;
		}

		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		readonly int n;
		Node Root;

		public SplitTree(int size, Monoid<TValue> monoid)
		{
			(op, iv) = (monoid.Op, monoid.Id);
			n = 1;
			while (n < size) n <<= 1;
			Clear();
		}
		public void Clear()
		{
			var q = new Queue<Node>();
			q.Enqueue(Root = new Node { Value = iv });

			for (int i = 1; i < n; ++i)
			{
				var node = q.Dequeue();
				q.Enqueue(node.Left = new Node { Value = iv });
				q.Enqueue(node.Right = new Node { Value = iv });
			}
		}

		public TValue this[int key] => Get(key);

		public TValue Get(int key)
		{
			var node = Split(key);
			return node.Value;
		}

		public void Set(int key, TValue value)
		{
			var node = Split(key);
			node.Value = op(value, node.Value);
		}

		public void Set(int l, int r, TValue value)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;
			Split(Root, 0, n, l, r, value);
		}

		Node Split(int key)
		{
			var node = Root;
			for (var f = n >> 1; f != 0; f >>= 1)
			{
				node.Left.Value = op(node.Value, node.Left.Value);
				node.Right.Value = op(node.Value, node.Right.Value);
				node.Value = iv;

				node = (key & f) == 0 ? node.Left : node.Right;
			}
			return node;
		}

		void Split(Node node, int nl, int nr, int l, int r, TValue value)
		{
			if (nl == l && nr == r)
			{
				node.Value = op(value, node.Value);
				return;
			}
			node.Left.Value = op(node.Value, node.Left.Value);
			node.Right.Value = op(node.Value, node.Right.Value);
			node.Value = iv;

			var nc = nl + nr >> 1;
			if (l < nc) Split(node.Left, nl, nc, l, nc < r ? nc : r, value);
			if (nc < r) Split(node.Right, nc, nr, l < nc ? nc : l, r, value);
		}
	}
}
