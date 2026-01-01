using TreesLib;

namespace TreesTest
{
	[TestClass]
	public class DirectedTreeTest
	{
		static readonly Random random = new Random();

		static void AreEquivalent_True_Single(int n)
		{
			var map = TestHelper.Shuffle(n);
			var tree1 = TestHelper.CreateDirectedTree(n, false);
			var tree2 = Array.ConvertAll(tree1, e => (u: map[e.u], v: map[e.v]));

			var actual = DirectedTreeHelper.AreEquivalent(n, tree1, tree2);
			Assert.IsTrue(actual);
		}

		// 不確実なテスト
		static void AreEquivalent_False_Single(int n)
		{
			var tree1 = TestHelper.CreateDirectedTree(n, false);
			var tree2 = TestHelper.CreateDirectedTree(n, false);

			var actual = DirectedTreeHelper.AreEquivalent(n, tree1, tree2);
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
			for (int i = 1; i < n; ++i) tree1[i - 1] = random.Next(2) == 0 ? (0, i) : (i, 0);

			var tree2 = Array.ConvertAll(tree1, e => (u: map[e.u], v: map[e.v]));

			var actual = DirectedTreeHelper.AreEquivalent(n, tree1, tree2);
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
			var es = TestHelper.CreateDirectedTree(n, false);

			// https://hello-world-494ec.firebaseapp.com/
			Console.WriteLine($"{n} {n - 1}");
			foreach (var (u, v) in es)
				Console.WriteLine($"{u} {v}");

			var tree = new DirectedTree(n, es);
			var nf = tree.GetNormalForm();
			Console.WriteLine($"Diameter: {tree.Diameter}");
			Console.WriteLine(nf);

			foreach (var p in (DirectedTree.Part[])[.. tree.Nodes, .. tree.Edges])
			{
				var form = p.GetForm();
				Console.WriteLine(form);
				Assert.IsTrue(DirectedTreeHelper.ToNormalForm(form) == nf);
			}
		}

		static void Parse_Test<TCenter>(string input) where TCenter : DirectedTree.Part
		{
			var tree = DirectedTree.Parse(input);
			DirectedTree.Part[] parts = [.. tree.Nodes, .. tree.Edges];

			var expected_nf = tree.GetNormalForm();
			var expected_forms = Array.ConvertAll(parts, p => p.GetForm());
			Console.WriteLine(expected_nf);
			Array.ForEach(expected_forms, Console.WriteLine);
			Array.ForEach(expected_forms, Test);

			for (int i = 0; i < expected_forms.Length; i++)
				for (int j = i + 1; j < expected_forms.Length; j++)
					Assert.IsTrue(DirectedTreeHelper.AreEquivalent(expected_forms[i], expected_forms[j]));

			void Test(string input)
			{
				var tree = DirectedTree.Parse(input);
				DirectedTree.Part[] parts = [.. tree.Nodes, .. tree.Edges];
				Assert.IsTrue(tree.Center is TCenter);
				Assert.AreEqual(expected_nf, tree.GetNormalForm());
				CollectionAssert.AreEquivalent(expected_forms, Array.ConvertAll(parts, p => p.GetForm()));
			}
		}

		[TestMethod]
		public void Parse_Node()
		{
			Parse_Test<DirectedTree.Node>("({()()}{(){}})");
			Parse_Test<DirectedTree.Node>("(()(){()}{()}{})");
		}

		[TestMethod]
		public void Parse_Edge()
		{
			Parse_Test<DirectedTree.Edge>("({()}){(){{}{}}}");
			Parse_Test<DirectedTree.Edge>("({{}}){{(){}}{}}");
		}
	}
}
