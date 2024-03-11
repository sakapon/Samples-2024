
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
		const int MinIndex = -1 << 30;
		const int MaxIndex = 1 << 30;
		Node Root;

		public void Clear() => Root = null;

		public long this[int key] => Get(key);

		public void Add(int l, int r, long value)
		{
			if (l < MinIndex) l = MinIndex;
			if (r > MaxIndex) r = MaxIndex;
			Add(ref Root, MinIndex, MaxIndex, l, r, value);
		}

		void Add(ref Node node, int nl, int nr, int l, int r, long value)
		{
			node ??= new Node();
			if (nl == l && nr == r) { node.Value += value; return; }
			var nc = nl + nr >> 1;
			if (l < nc) Add(ref node.Left, nl, nc, l, nc < r ? nc : r, value);
			if (nc < r) Add(ref node.Right, nc, nr, l < nc ? nc : l, r, value);
		}

		public long Get(int key)
		{
			var v = 0L;
			var node = Root;
			var (nl, nr) = (MinIndex, MaxIndex);
			while (true)
			{
				if (node == null) return v;
				v += node.Value;
				if (nl + 1 == nr) return v;
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
				if (nl + 1 == nr) { node.Value += value; return; }
				var nc = nl + nr >> 1;
				if (key < nc) { nr = nc; node = ref node.Left; }
				else { nl = nc; node = ref node.Right; }
			}
		}
	}
}
