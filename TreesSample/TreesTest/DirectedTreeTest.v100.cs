using TreesLib.v100;

namespace TreesTest.v100
{
	[TestClass]
	public class DirectedTreeTest
	{
		[TestMethod]
		public void GetForm_6_1()
		{
			var edges = new[]
			{
				(0, 1),
				(0, 2),
				(2, 4),
				(2, 5),
				(3, 5),
			};

			Assert.AreEqual("0(+()+(+()+(-())))", DirectedTree.GetFormForVertex(edges, 0));
			Assert.AreEqual("0(-(+(+()+(-()))))", DirectedTree.GetFormForVertex(edges, 1));
			Assert.AreEqual("0(+()+(-())-(+()))", DirectedTree.GetFormForVertex(edges, 2));
			Assert.AreEqual("0(+(-(+()-(+()))))", DirectedTree.GetFormForVertex(edges, 3));
			Assert.AreEqual("0(-(+(-())-(+())))", DirectedTree.GetFormForVertex(edges, 4));
			Assert.AreEqual("0(-()-(+()-(+())))", DirectedTree.GetFormForVertex(edges, 5));
		}

		[TestMethod]
		public void GetForm_7()
		{
			var edges = new[]
			{
				(0, 1),
				(0, 2),
				(3, 1),
				(4, 1),
				(5, 2),
				(2, 6),
			};

			Assert.AreEqual("0(+(+()-())+(-()-()))", DirectedTree.GetFormForVertex(edges, 0));
			Assert.AreEqual("0(-()-()-(+(+()-())))", DirectedTree.GetFormForVertex(edges, 1));
			Assert.AreEqual("0(+()-()-(+(-()-())))", DirectedTree.GetFormForVertex(edges, 2));
			Assert.AreEqual("0(+(-()-(+(+()-()))))", DirectedTree.GetFormForVertex(edges, 3));
			Assert.AreEqual("0(+(-()-(+(+()-()))))", DirectedTree.GetFormForVertex(edges, 4));
			Assert.AreEqual("0(+(+()-(+(-()-()))))", DirectedTree.GetFormForVertex(edges, 5));
			Assert.AreEqual("0(-(-()-(+(-()-()))))", DirectedTree.GetFormForVertex(edges, 6));
		}

		[TestMethod]
		public void Parse_6_2()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (0, 4), (0, 5) }, UndirectedTree.Parse("(((()))()())"));
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (3, 4), (1, 5) }, UndirectedTree.Parse("((((()))()))"));
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (1, 3), (0, 4), (4, 5) }, UndirectedTree.Parse("((()())(()))"));
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (2, 4), (0, 5) }, UndirectedTree.Parse("(((()()))())"));
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (3, 4), (3, 5) }, UndirectedTree.Parse("((((()()))))"));
		}

		[TestMethod]
		public void Parse_7()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (1, 3), (0, 4), (4, 5), (4, 6) }, UndirectedTree.Parse("((()())(()()))"));
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (2, 4), (0, 5), (0, 6) }, UndirectedTree.Parse("(((()()))()())"));
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (3, 4), (3, 5), (1, 6) }, UndirectedTree.Parse("((((()()))()))"));
		}
	}
}
