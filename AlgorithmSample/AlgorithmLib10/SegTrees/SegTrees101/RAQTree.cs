
namespace AlgorithmLib10.SegTrees.SegTrees101
{
	public class RAQTree
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

		public RAQTree(int size)
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

		public long this[int key] => Get(key);

		public long Get(int key)
		{
			GetNode(key);
			var v = 0L;
			foreach (var n in Path) v += n.Value;
			return v;
		}

		public void Add(int key, long value)
		{
			var node = GetNode(key);
			node.Value += value;
		}

		public void Add(int l, int r, long value)
		{
			ScanNodes(l, r);
			foreach (var n in Path) n.Value += value;
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
