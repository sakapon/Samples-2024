using TreesLib;

namespace TreesTest
{
	[TestClass]
	public class UndirectedTreeTest
	{
		static readonly Random random = new Random();

		static void AreEquivalent_True_Single(int n)
		{
			var map = TestHelper.Shuffle(n);
			var tree1 = TestHelper.CreateTree(n, false);
			var tree2 = Array.ConvertAll(tree1, e => (u: map[e.u], v: map[e.v]));

			var actual = UndirectedTreeHelper.AreEquivalent(n, tree1, tree2);
			Assert.IsTrue(actual);
		}

		// 不確実なテスト
		static void AreEquivalent_False_Single(int n)
		{
			var tree1 = TestHelper.CreateTree(n, false);
			var tree2 = TestHelper.CreateTree(n, false);

			var actual = UndirectedTreeHelper.AreEquivalent(n, tree1, tree2);
			Assert.IsFalse(actual);
		}

		[TestMethod]
		public void AreEquivalent_True_Many()
		{
			for (int k = 0; k < 1000; k++)
			{
				var n = random.Next(100) + 1;
				AreEquivalent_True_Single(n);
			}
		}

		[TestMethod]
		public void AreEquivalent_True_Time()
		{
			const int n = 200000;
			AreEquivalent_True_Single(n);
		}

		[TestMethod]
		public void AreEquivalent_True_Star()
		{
			const int n = 200000;
			var map = TestHelper.Shuffle(n);

			var tree1 = new (int u, int v)[n - 1];
			for (int i = 1; i < n; ++i) tree1[i - 1] = (0, i);

			var tree2 = Array.ConvertAll(tree1, e => (u: map[e.u], v: map[e.v]));

			var actual = UndirectedTreeHelper.AreEquivalent(n, tree1, tree2);
			Assert.IsTrue(actual);
		}

		[TestMethod]
		public void AreEquivalent_False()
		{
			const int n = 100;
			AreEquivalent_False_Single(n);
		}

		[TestMethod]
		public void GetForm_AllParts()
		{
			var n = 8;
			var es = TestHelper.CreateTree(n, false);

			// https://hello-world-494ec.firebaseapp.com/
			Console.WriteLine($"{n} {n - 1}");
			foreach (var (u, v) in es)
				Console.WriteLine($"{u} {v}");

			var tree = new UndirectedTree(n, es);
			var nf = tree.GetNormalForm();
			Console.WriteLine($"Diameter: {tree.Diameter}");
			Console.WriteLine(nf);

			foreach (var p in (UndirectedTree.Part[])[.. tree.Nodes, .. tree.Edges])
			{
				var form = p.GetForm();
				Console.WriteLine(form);
				Assert.IsTrue(UndirectedTreeHelper.ToNormalForm(form) == nf);
			}
		}

		[TestMethod]
		public void Parse_Node()
		{
			var nf = "((()()())(()))";
			var tree = UndirectedTree.Parse(nf);
			Assert.IsTrue(tree.Center is UndirectedTree.Node);
			Console.WriteLine(nf);

			foreach (var p in (UndirectedTree.Part[])[.. tree.Nodes, .. tree.Edges])
			{
				var form = p.GetForm();
				Console.WriteLine(form);
				Assert.IsTrue(UndirectedTreeHelper.ToNormalForm(form) == nf);
			}
		}

		[TestMethod]
		public void Parse_Edge()
		{
			var nf = "((())())((())())";
			var tree = UndirectedTree.Parse(nf);
			Assert.IsTrue(tree.Center is UndirectedTree.Edge);
			Console.WriteLine(nf);

			foreach (var p in (UndirectedTree.Part[])[.. tree.Nodes, .. tree.Edges])
			{
				var form = p.GetForm();
				Console.WriteLine(form);
				Assert.IsTrue(UndirectedTreeHelper.ToNormalForm(form) == nf);
			}
		}
	}
}
