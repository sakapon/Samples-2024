
namespace AlgorithmLib10.SegTrees.SegTrees204
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

		public void Clear() => Root = null;

		public long this[int key] => Get(key);
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, MinIndex, MaxIndex, l, r);
		}

		long Get(Node node, int nl, int nr, int l, int r)
		{
			if (node == null) return 0;
			if (nl == l && nr == r) return node.Value;
			var nc = nl + nr >> 1;
			var v = l < nc ? Get(node.Left, nl, nc, l, nc < r ? nc : r) : 0;
			return nc < r ? v + Get(node.Right, nc, nr, l < nc ? nc : l, r) : v;
		}

		public long Get(int key)
		{
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == null) return 0;
				if (nl + 1 == nr) return node.Value;
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = node.Left; }
				else { nl = nc; node = node.Right; }
			}
		}

		public void Add(int key, long value)
		{
			ref var node = ref Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				node ??= new Node();
				node.Value += value;
				if (nl + 1 == nr) return;
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref node.Left; }
				else { nl = nc; node = ref node.Right; }
			}
		}
	}
}
