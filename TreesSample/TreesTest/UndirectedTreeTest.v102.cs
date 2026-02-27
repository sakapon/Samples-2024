using TreesLib.v102;

namespace TreesTest.v102
{
	[TestClass]
	public class UndirectedTreeTest
	{
		[TestMethod]
		public void GetForm_6_1()
		{
			var edges = new[]
			{
				(0, 1),
				(1, 2),
				(1, 3),
				(2, 4),
				(3, 5),
			};
			var tree = new UndirectedTree(edges);

			Assert.AreEqual("(((())(())))", tree.GetFormForVertex(0));
			Assert.AreEqual("((())(())())", tree.GetFormForVertex(1));
			Assert.AreEqual("(((())())())", tree.GetFormForVertex(2));
			Assert.AreEqual("(((())())())", tree.GetFormForVertex(3));
			Assert.AreEqual("((((())())))", tree.GetFormForVertex(4));
			Assert.AreEqual("((((())())))", tree.GetFormForVertex(5));

			Assert.AreEqual("((())(()))()", tree.GetFormForEdge(0));
			Assert.AreEqual("((())())(())", tree.GetFormForEdge(1));
			Assert.AreEqual("((())())(())", tree.GetFormForEdge(2));
			Assert.AreEqual("(((())()))()", tree.GetFormForEdge(3));
			Assert.AreEqual("(((())()))()", tree.GetFormForEdge(4));

			// Center: Vertex 1
			Assert.AreEqual("((())(())())", tree.GetNormalForm());
		}

		[TestMethod]
		public void GetForm_6_2()
		{
			var edges = new[]
			{
				(0, 1),
				(0, 2),
				(0, 3),
				(3, 4),
				(4, 5),
			};
			var tree = new UndirectedTree(edges);

			Assert.AreEqual("(((()))()())", tree.GetFormForVertex(0));
			Assert.AreEqual("((((()))()))", tree.GetFormForVertex(1));
			Assert.AreEqual("((((()))()))", tree.GetFormForVertex(2));
			Assert.AreEqual("((()())(()))", tree.GetFormForVertex(3));
			Assert.AreEqual("(((()()))())", tree.GetFormForVertex(4));
			Assert.AreEqual("((((()()))))", tree.GetFormForVertex(5));

			Assert.AreEqual("(((()))())()", tree.GetFormForEdge(0));
			Assert.AreEqual("(((()))())()", tree.GetFormForEdge(1));
			Assert.AreEqual("((()))(()())", tree.GetFormForEdge(2));
			Assert.AreEqual("((()()))(())", tree.GetFormForEdge(3));
			Assert.AreEqual("(((()())))()", tree.GetFormForEdge(4));

			// Center: Vertex 3
			Assert.AreEqual("((()())(()))", tree.GetNormalForm());
		}

		[TestMethod]
		public void GetForm_6_3()
		{
			var edges = new[]
			{
				(0, 1),
				(0, 2),
				(0, 3),
				(0, 4),
				(4, 5),
			};
			var tree = new UndirectedTree(edges);

			Assert.AreEqual("((())()()())", tree.GetFormForVertex(0));
			Assert.AreEqual("(((())()()))", tree.GetFormForVertex(1));
			Assert.AreEqual("(((())()()))", tree.GetFormForVertex(2));
			Assert.AreEqual("(((())()()))", tree.GetFormForVertex(3));
			Assert.AreEqual("((()()())())", tree.GetFormForVertex(4));
			Assert.AreEqual("(((()()())))", tree.GetFormForVertex(5));

			Assert.AreEqual("((())()())()", tree.GetFormForEdge(0));
			Assert.AreEqual("((())()())()", tree.GetFormForEdge(1));
			Assert.AreEqual("((())()())()", tree.GetFormForEdge(2));
			Assert.AreEqual("(()()())(())", tree.GetFormForEdge(3));
			Assert.AreEqual("((()()()))()", tree.GetFormForEdge(4));

			// Center: Edge 3
			Assert.AreEqual("(()()())(())", tree.GetNormalForm());
		}

		[TestMethod]
		public void GetForm_7_1()
		{
			var edges = new[]
			{
				(0, 1),
				(0, 2),
				(1, 3),
				(1, 4),
				(2, 5),
				(2, 6),
			};
			var tree = new UndirectedTree(edges);

			Assert.AreEqual("((()())(()()))", tree.GetFormForVertex(0));
			Assert.AreEqual("(((()()))()())", tree.GetFormForVertex(1));
			Assert.AreEqual("(((()()))()())", tree.GetFormForVertex(2));
			Assert.AreEqual("((((()()))()))", tree.GetFormForVertex(3));
			Assert.AreEqual("((((()()))()))", tree.GetFormForVertex(4));
			Assert.AreEqual("((((()()))()))", tree.GetFormForVertex(5));
			Assert.AreEqual("((((()()))()))", tree.GetFormForVertex(6));

			Assert.AreEqual("((()()))(()())", tree.GetFormForEdge(0));
			Assert.AreEqual("((()()))(()())", tree.GetFormForEdge(1));
			Assert.AreEqual("(((()()))())()", tree.GetFormForEdge(2));
			Assert.AreEqual("(((()()))())()", tree.GetFormForEdge(3));
			Assert.AreEqual("(((()()))())()", tree.GetFormForEdge(4));
			Assert.AreEqual("(((()()))())()", tree.GetFormForEdge(5));

			// Center: Vertex 0
			Assert.AreEqual("((()())(()()))", tree.GetNormalForm());
		}

		[TestMethod]
		public void Parse_6_2()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (0, 4), (0, 5) }, UndirectedTree.Parse("(((()))()())").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (3, 4), (1, 5) }, UndirectedTree.Parse("((((()))()))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (1, 3), (0, 4), (4, 5) }, UndirectedTree.Parse("((()())(()))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (2, 4), (0, 5) }, UndirectedTree.Parse("(((()()))())").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (3, 4), (3, 5) }, UndirectedTree.Parse("((((()()))))").edges);

			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (0, 4), (0, 5) }, UndirectedTree.Parse("(((()))())()").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (3, 4), (3, 5), (0, 3) }, UndirectedTree.Parse("((()))(()())").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (1, 3), (4, 5), (0, 4) }, UndirectedTree.Parse("((()()))(())").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (2, 4), (0, 5) }, UndirectedTree.Parse("(((()())))()").edges);
		}

		[TestMethod]
		public void Parse_7()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (1, 3), (0, 4), (4, 5), (4, 6) }, UndirectedTree.Parse("((()())(()()))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (2, 4), (0, 5), (0, 6) }, UndirectedTree.Parse("(((()()))()())").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (3, 4), (3, 5), (1, 6) }, UndirectedTree.Parse("((((()()))()))").edges);

			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (1, 3), (4, 5), (4, 6), (0, 4) }, UndirectedTree.Parse("((()()))(()())").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (2, 4), (0, 5), (0, 6) }, UndirectedTree.Parse("(((()()))())()").edges);
		}
	}
}
