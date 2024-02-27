
namespace AlgorithmLib10.DataTrees.BSTs.BSTs104
{
	public class Int32RSQTree
	{
		[System.Diagnostics.DebuggerDisplay(@"Value = {Value}")]
		public class Node
		{
			public long Value;
			public Node Left, Right;
		}

		// [-1 << MaxDigit, 1 << MaxDigit)
		const int MaxDigit = 30;
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
			ScanNode(Root, -1 << MaxDigit, 1 << MaxDigit, l, r);
		}

		void ScanNode(Node node, int nl, int nr, int l, int r)
		{
			if (node == null) return;
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = (nl + nr) >> 1;
			if (l < nc) ScanNode(node.Left, nl, nc, l, nc < r ? nc : r);
			if (nc < r) ScanNode(node.Right, nc, nr, l < nc ? nc : l, r);
		}

		void AddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			var nc = 0;
			for (int d = MaxDigit - 1; d >= -2; --d)
			{
				node ??= new Node();
				Path.Add(node);
				if (key < nc)
				{
					node = ref node.Left;
					nc -= 1 << d;
				}
				else
				{
					node = ref node.Right;
					nc |= 1 << d;
				}
			}
		}
	}
}
