using TreesLib.v100;

namespace TreesTest.v100
{
	[TestClass]
	public class UndirectedTreeTest
	{
		[TestMethod]
		public void GetFormForVertex_6()
		{
			(int u, int v)[] edges =
			[
				(0, 1),
				(0, 2),
				(0, 3),
				(3, 4),
				(4, 5),
			];

			Assert.AreEqual("(((()))()())", UndirectedTree.GetFormForVertex(edges, 0));
			Assert.AreEqual("((((()))()))", UndirectedTree.GetFormForVertex(edges, 1));
			Assert.AreEqual("((((()))()))", UndirectedTree.GetFormForVertex(edges, 2));
			Assert.AreEqual("((()())(()))", UndirectedTree.GetFormForVertex(edges, 3));
			Assert.AreEqual("(((()()))())", UndirectedTree.GetFormForVertex(edges, 4));
			Assert.AreEqual("((((()()))))", UndirectedTree.GetFormForVertex(edges, 5));
		}

		[TestMethod]
		public void GetFormForVertex_7()
		{
			(int u, int v)[] edges =
			[
				(0, 1),
				(0, 2),
				(1, 3),
				(1, 4),
				(2, 5),
				(2, 6),
			];

			Assert.AreEqual("((()())(()()))", UndirectedTree.GetFormForVertex(edges, 0));
			Assert.AreEqual("(((()()))()())", UndirectedTree.GetFormForVertex(edges, 1));
			Assert.AreEqual("(((()()))()())", UndirectedTree.GetFormForVertex(edges, 2));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 3));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 4));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 5));
			Assert.AreEqual("((((()()))()))", UndirectedTree.GetFormForVertex(edges, 6));
		}
	}
}
