using TreesLib.v101;

namespace TreesTest.v101
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
		}
	}
}
