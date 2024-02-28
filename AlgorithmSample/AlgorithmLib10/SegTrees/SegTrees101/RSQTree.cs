
namespace AlgorithmLib10.SegTrees.SegTrees101
{
	public class RSQTree
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public long Value;
			public Node Left, Right;
		}

		readonly int n;
		Node Root;
		readonly List<Node> Path = new List<Node>();

		public RSQTree(int size)
		{
			n = 1;
			while (n < size) n <<= 1;
			Clear();
		}
		public void Clear()
		{
			var q = new Queue<Node>();
			q.Enqueue(Root = new Node());

			for (int i = 1; i < n; ++i)
			{
				var node = q.Dequeue();
				q.Enqueue(node.Left = new Node());
				q.Enqueue(node.Right = new Node());
			}
		}

		public long this[int key]
		{
			get => Get(key);
			set => Set(key, value);
		}
		public long this[int l, int r] => Get(l, r);

		public long Get(int key)
		{
			var node = GetNode(key);
			return node.Value;
		}

		public long Get(int l, int r)
		{
			ScanNodes(l, r);
			var v = 0L;
			foreach (var n in Path) v += n.Value;
			return v;
		}

		public void Add(int key, long value)
		{
			GetNode(key);
			foreach (var n in Path) n.Value += value;
		}

		public void Set(int key, long value)
		{
			var node = GetNode(key);
			var d = value - node.Value;
			foreach (var n in Path) n.Value += d;
		}

		void ScanNodes(int l, int r)
		{
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
			var (nl, nr) = (0, n);
			while (node != null)
			{
				Path.Add(node);
				if (node.Left == null) return node;
				var nc = nl + nr >> 1;
				if (key < nc)
				{
					node = node.Left;
					nr = nc;
				}
				else
				{
					node = node.Right;
					nl = nc;
				}
			}
			return null;
		}
	}
}
