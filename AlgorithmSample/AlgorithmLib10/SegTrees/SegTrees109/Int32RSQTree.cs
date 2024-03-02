
namespace AlgorithmLib10.SegTrees.SegTrees109
{
	public class Int32RSQTree
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

		public long this[int key] => this[key, key + 1];
		public long this[int l, int r]
		{
			get
			{
				ScanNode(l, r);
				var v = 0L;
				foreach (var n in Path) v += n.Value;
				return v;
			}
		}

		public void Add(int key, long value)
		{
			AddNode(key);
			foreach (var n in Path) n.Value += value;
		}

		void ScanNode(int l, int r)
		{
			Path.Clear();
			ScanNode(Root, MinIndex, MaxIndex, l, r);
		}

		void ScanNode(Node node, int nl, int nr, int l, int r)
		{
			if (node == null) return;
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = nl + nr >> 1;
			if (l < nc) ScanNode(node.Left, nl, nc, l, nc < r ? nc : r);
			if (nc < r) ScanNode(node.Right, nc, nr, l < nc ? nc : l, r);
		}

		void AddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				node ??= new Node();
				Path.Add(node);
				if (nl + 1 == nr) return;
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref node.Left; }
				else { nl = nc; node = ref node.Right; }
			}
		}
	}
}
