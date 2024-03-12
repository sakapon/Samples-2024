
namespace AlgorithmLib10.SegTrees.SegTrees103
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
		const int MinIndex = -1 << 30;
		const int MaxIndex = 1 << 30;
		Node Root;

		public void Clear() => Root = null;

		public long this[int key] => Get(key);
		public long this[int l, int r] => Get(l, r);

		public long Get(int l, int r)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			return Get(Root, MinIndex, MaxIndex, l, r);

			long Get(Node node, int nl, int nr, int l, int r)
			{
				if (node == null) return 0;
				if (nl == l && nr == r) return node.Value;
				var nc = nl + nr >> 1;
				var v = l < nc ? Get(node.Left, nl, nc, l, nc < r ? nc : r) : 0;
				return nc < r ? v + Get(node.Right, nc, nr, l < nc ? nc : l, r) : v;
			}
		}

		public long Get(int key)
		{
			return Get(Root, MinIndex, MaxIndex, key);

			long Get(Node node, int nl, int nr, int key)
			{
				if (node == null) return 0;
				if (nl + 1 == nr) return node.Value;
				var nc = nl + nr >> 1;
				return key < nc ? Get(node.Left, nl, nc, key) : Get(node.Right, nc, nr, key);
			}
		}

		public void Add(int key, long value)
		{
			Add(ref Root, MinIndex, MaxIndex, key, value);

			void Add(ref Node node, int nl, int nr, int key, long value)
			{
				node ??= new Node();
				node.Value += value;
				if (nl + 1 == nr) return;
				var nc = nl + nr >> 1;
				if (key < nc) Add(ref node.Left, nl, nc, key, value);
				else Add(ref node.Right, nc, nr, key, value);
			}
		}
	}
}
