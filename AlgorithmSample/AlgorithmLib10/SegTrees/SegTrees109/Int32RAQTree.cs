
namespace AlgorithmLib10.SegTrees.SegTrees109
{
	public class Int32RAQTree
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public long Value;
			public Node Left, Right;
		}

		// [MinIndex, MaxIndex)
		const int MinIndex = -1 << 30, MaxIndex = 1 << 30;
		Node Root;
		readonly List<Node> Path = new List<Node>();

		public void Clear() => Root = null;

		public long this[int key]
		{
			get
			{
				ScanNode(key);
				var v = 0L;
				foreach (var n in Path) v += n.Value;
				return v;
			}
		}

		public void Add(int key, long value) => Add(key, key + 1, value);
		public void Add(int l, int r, long value)
		{
			AddNode(l, r);
			foreach (var n in Path) n.Value += value;
		}

		void AddNode(int l, int r)
		{
			Path.Clear();
			AddNode(ref Root, MinIndex, MaxIndex, l, r);
		}

		void AddNode(ref Node node, int nl, int nr, int l, int r)
		{
			node ??= new Node();
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = nl + nr >> 1;
			if (l < nc) AddNode(ref node.Left, nl, nc, l, nc < r ? nc : r);
			if (nc < r) AddNode(ref node.Right, nc, nr, l < nc ? nc : l, r);
		}

		void ScanNode(int key)
		{
			Path.Clear();
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == null) return;
				Path.Add(node);
				if (nl + 1 == nr) break;
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
		}
	}
}
