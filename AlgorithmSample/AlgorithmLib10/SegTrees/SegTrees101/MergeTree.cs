
namespace AlgorithmLib10.SegTrees.SegTrees101
{
	public class MergeTree<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public TValue Value;
			public Node Left, Right;
		}

		readonly int n;
		readonly Func<TValue, TValue, TValue> op;
		readonly TValue iv;
		Node Root;
		readonly List<Node> Path = new List<Node>();

		public MergeTree(int size, Monoid<TValue> monoid)
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

		public TValue this[int key]
		{
			get => Get(key);
			set => Set(key, value);
		}
		public TValue this[int l, int r] => Get(l, r);

		public TValue Get(int key)
		{
			var node = GetNode(key);
			return node.Value;
		}

		public TValue Get(int l, int r)
		{
			ScanNodes(l, r);
			var v = iv;
			foreach (var n in Path) v = op(v, n.Value);
			return v;
		}

		public void Set(int key, TValue value)
		{
			var node = GetNode(key);
			node.Value = value;
			for (int i = Path.Count - 2; i >= 0; --i)
			{
				node = Path[i];
				node.Value = op(node.Left.Value, node.Right.Value);
			}
		}

		void ScanNodes(int l, int r)
		{
			if (l < 0) l = 0;
			if (r > n) r = n;
			Path.Clear();
			ScanNodes(Root, 0, n, l, r);
		}

		void ScanNodes(Node node, int nl, int nr, int l, int r)
		{
			if (node == null) return;
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = nl + nr >> 1;
			if (l < nc) ScanNodes(node.Left, nl, nc, l, nc < r ? nc : r);
			if (nc < r) ScanNodes(node.Right, nc, nr, l < nc ? nc : l, r);
		}

		Node GetNode(int key)
		{
			Path.Clear();
			var node = Root;
			for (var f = n >> 1; ; f >>= 1)
			{
				Path.Add(node);
				if (f == 0) return node;
				node = (key & f) == 0 ? node.Left : node.Right;
			}
		}
	}
}
