
namespace AlgorithmLib10.SegTrees.SegTrees104
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

		public long this[int key]
		{
			get => Get(key);
			set => Set(key, value);
		}
		public long this[int l, int r] => Get(l, r);

		public long Get(int key)
		{
			var node = GetNode(key);
			return node != null ? node.Value : 0;
		}

		public long Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, MinIndex, MaxIndex, l, r);
		}

		public void Set(int key, long value)
		{
			var node = GetOrAddNode(key);
			var d = value - node.Value;
			foreach (var n in Path) n.Value += d;
		}

		public void Add(int key, long value)
		{
			GetOrAddNode(key);
			foreach (var n in Path) n.Value += value;
		}

		long Get(Node node, int nl, int nr, int l, int r)
		{
			if (node == null) return 0;
			if (nl == l && nr == r) return node.Value;
			var nc = nl + nr >> 1;
			var v = l < nc ? Get(node.Left, nl, nc, l, nc < r ? nc : r) : 0;
			return nc < r ? v + Get(node.Right, nc, nr, l < nc ? nc : l, r) : v;
		}

		Node GetNode(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == null) break;
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
			return node;
		}

		Node GetOrAddNode(int key)
		{
			Path.Clear();
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				node ??= new Node();
				Path.Add(node);
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
