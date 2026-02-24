using TreesLib.v100;

namespace TreesTest.v100
{
	[TestClass]
	public class UndirectedTreeTest
	{
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

			Assert.AreEqual("(((()))()())", UndirectedTree.GetFormForVertex(edges, 0));
			Assert.AreEqual("((((()))()))", UndirectedTree.GetFormForVertex(edges, 1));
			Assert.AreEqual("((((()))()))", UndirectedTree.GetFormForVertex(edges, 2));
			Assert.AreEqual("((()())(()))", UndirectedTree.GetFormForVertex(edges, 3));
			Assert.AreEqual("(((()()))())", UndirectedTree.GetFormForVertex(edges, 4));
			Assert.AreEqual("((((()()))))", UndirectedTree.GetFormForVertex(edges, 5));
		}

		[TestMethod]
		public void GetForm_7()
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

			Assert.AreEqual("((()())(()()))", UndirectedTree.GetFormForVertex(edges, 0));
			Assert.AreEqual("(((()()))()())", UndirectedTree.GetFormForVertex(edges, 1));
			Assert.AreEqual("(((()()))()())", UndirectedTree.GetFormForVertex(edges, 2));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 3));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 4));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 5));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 6));
		}

		[TestMethod]
		public void Parse_6_2()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (2, 3), (0, 4), (0, 5) }, UndirectedTree.Parse("(((()))()())"));
		}
	}
}
