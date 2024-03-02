
namespace AlgorithmLib10.SegTrees.SegTrees104
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

		public long this[int key] => Get(key);

		public long Get(int key)
		{
			ScanNodes(key);
			var v = 0L;
			foreach (var n in Path) v += n.Value;
			return v;
		}

		public void Add(int key, long value)
		{
			var node = GetOrAddNode(key);
			node.Value += value;
		}

		public void Add(int l, int r, long value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Path.Clear();
			AddNodes(ref Root, MinIndex, MaxIndex, l, r);
			foreach (var n in Path) n.Value += value;
		}

		void AddNodes(ref Node node, int nl, int nr, int l, int r)
		{
			node ??= new Node();
			if (nl == l && nr == r) { Path.Add(node); return; }
			var nc = nl + nr >> 1;
			if (l < nc) AddNodes(ref node.Left, nl, nc, l, nc < r ? nc : r);
			if (nc < r) AddNodes(ref node.Right, nc, nr, l < nc ? nc : l, r);
		}

		void ScanNodes(int key)
		{
			Path.Clear();
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == null) return;
				Path.Add(node);
				if (nl + 1 == nr) return;
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

		Node GetOrAddNode(int key)
		{
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				node ??= new Node();
				if (nl + 1 == nr) break;
				var nc = nl + nr >> 1;
				if (key < nc)
				{
					node = ref node.Left;
					nr = nc;
				}
				else
				{
					node = ref node.Right;
					nl = nc;
				}
			}
			return node;
		}
	}
}
