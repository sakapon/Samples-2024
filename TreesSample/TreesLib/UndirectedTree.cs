namespace TreesLib
{
	public static class UndirectedTreeHelper
	{
		public static string ToNormalForm(string form) => UndirectedTree.Parse(form).GetNormalForm();
		public static bool AreEquivalent(string form1, string form2) => form1 == form2 || ToNormalForm(form1) == ToNormalForm(form2);
		public static bool AreEquivalent(int n, (int u, int v)[] edges1, (int u, int v)[] edges2)
		{
			var tree1 = new UndirectedTree(n, edges1);
			var tree2 = new UndirectedTree(n, edges2);
			return tree1.GetNormalForm() == tree2.GetNormalForm();
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"{Nodes.Length} nodes, Diameter = {Diameter}, Center = {Center}")]
	public class UndirectedTree
	{
		const char EO = '(', EC = ')';
		static readonly StringComparer FormComparer = StringComparer.Ordinal;

		#region Static Methods

		// 文字列表現のルートが頂点か辺かを判定します。
		public static bool IsForNode(string form)
		{
			if (form == null) throw new ArgumentNullException(nameof(form));

			var opened = 0;
			var count = 0;

			foreach (var c in form)
			{
				switch (c)
				{
					case EO:
						++opened;
						break;
					case EC:
						if (opened == 0) throw new FormatException();
						if (--opened == 0) ++count;
						break;
					default:
						throw new FormatException();
				}
			}

			if (opened > 0) throw new FormatException();
			if (count == 1) return true;
			if (count == 2) return false;
			throw new FormatException();
		}

		// form: 標準形とは限りません。
		public static UndirectedTree Parse(string form) => ParseWithRoot(form).tree;
		public static (UndirectedTree tree, Part root) ParseWithRoot(string form)
		{
			if (form == null) throw new ArgumentNullException(nameof(form));

			var edges = new List<(int, int)>();
			var roots = new List<int>();
			var vi = -1;
			var q = new Stack<int>();

			foreach (var c in form)
			{
				switch (c)
				{
					case EO:
						if (q.Count == 0) roots.Add(++vi);
						else edges.Add((q.Peek(), ++vi));
						q.Push(vi);
						break;
					case EC:
						if (q.Count == 0) throw new FormatException();
						q.Pop();
						break;
					default:
						throw new FormatException();
				}
			}

			if (q.Count > 0) throw new FormatException();
			if (roots.Count == 1)
			{
				var tree = new UndirectedTree(vi + 1, edges.ToArray());
				return (tree, tree.Nodes[0]);
			}
			if (roots.Count == 2)
			{
				edges.Add((roots[0], roots[1]));
				var tree = new UndirectedTree(vi + 1, edges.ToArray());
				return (tree, tree.Edges[^1]);
			}
			throw new FormatException();
		}
		#endregion

		#region Subclasses

		public abstract record Part(int Id)
		{
			public abstract string GetForm();
		}

		[System.Diagnostics.DebuggerDisplay(@"Node {Id}: {Edges.Count} edges, Depth = {Depth}")]
		public record Node(int Id) : Part(Id)
		{
			public List<Edge> Edges = [];
			public int Depth = -1;
			public Edge Parent;

			public override string GetForm() => GetFormRec(null);
			internal string GetFormRec(Edge parent)
			{
				var l = new List<string>();
				foreach (var e in Edges)
				{
					if (e == parent) continue;
					var nv = e.GetNextNode(this);
					l.Add(nv.GetFormRec(e));
				}

				l.Sort(FormComparer);
				var f = string.Join("", l);
				return $"{EO}{f}{EC}";
			}
		}

		[System.Diagnostics.DebuggerDisplay(@"Edge {Id}: Node {From.Id} - Node {To.Id}")]
		public record Edge(int Id, Node From, Node To) : Part(Id)
		{
			public Node GetNextNode(Node from) => from == From ? To : From;

			public override string GetForm()
			{
				var f1 = From.GetFormRec(this);
				var f2 = To.GetFormRec(this);
				if (FormComparer.Compare(f1, f2) > 0) (f1, f2) = (f2, f1);
				return f1 + f2;
			}
		}
		#endregion

		public Node[] Nodes { get; }
		public Edge[] Edges { get; }

		public Node Root { get; private set; }
		public int Diameter { get; private set; }
		public Part Center { get; private set; }

		#region Instance Methods

		public UndirectedTree(int nodesCount, (int u, int v)[] edges)
		{
			if (nodesCount <= 0) throw new ArgumentException("The number of nodes must be positive.", nameof(nodesCount));
			if (edges.Length != nodesCount - 1) throw new ArgumentException("The number of edges is invalid.", nameof(edges));
			// 連結性は判定しません。

			Nodes = new Node[nodesCount];
			for (int vi = 0; vi < nodesCount; ++vi) Nodes[vi] = new Node(vi);

			Edges = new Edge[edges.Length];
			for (int ei = 0; ei < edges.Length; ++ei)
			{
				var (u, v) = edges[ei];
				var e = Edges[ei] = new Edge(ei, Nodes[u], Nodes[v]);
				e.From.Edges.Add(e);
				e.To.Edges.Add(e);
			}

			FindCenter();
		}

		public void Reroot(Node root)
		{
			Root = root;
			root.Depth = 0;
			root.Parent = null;
			DFS(root);

			static void DFS(Node v)
			{
				foreach (var e in v.Edges)
				{
					if (e == v.Parent) continue;
					var nv = e.GetNextNode(v);
					nv.Depth = v.Depth + 1;
					nv.Parent = e;
					DFS(nv);
				}
			}
		}

		void FindCenter()
		{
			Reroot(Nodes[0]);
			var tv = Nodes.MaxBy(v => v.Depth);
			Reroot(tv);
			tv = Nodes.MaxBy(v => v.Depth);

			Diameter = tv.Depth;
			var radius = (Diameter + 1) / 2;
			while (tv.Depth > radius) tv = tv.Parent.GetNextNode(tv);
			Center = Diameter % 2 == 0 ? tv : tv.Parent;
		}

		public string GetNormalForm() => Center.GetForm();
		#endregion
	}
}
