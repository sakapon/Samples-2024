using TreesLib.v102;

namespace TreesTest.v102
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
			var tree = new DirectedTree(edges);

			Assert.AreEqual("0(+()+(+()+(-())))", tree.GetFormForVertex(0));
			Assert.AreEqual("0(-(+(+()+(-()))))", tree.GetFormForVertex(1));
			Assert.AreEqual("0(+()+(-())-(+()))", tree.GetFormForVertex(2));
			Assert.AreEqual("0(+(-(+()-(+()))))", tree.GetFormForVertex(3));
			Assert.AreEqual("0(-(+(-())-(+())))", tree.GetFormForVertex(4));
			Assert.AreEqual("0(-()-(+()-(+())))", tree.GetFormForVertex(5));

			Assert.AreEqual("+()-(+(+()+(-())))", tree.GetFormForEdge(0));
			Assert.AreEqual("+(+()+(-()))-(+())", tree.GetFormForEdge(1));
			Assert.AreEqual("+()-(+(-())-(+()))", tree.GetFormForEdge(2));
			Assert.AreEqual("+(-())-(+()-(+()))", tree.GetFormForEdge(3));
			Assert.AreEqual("+(-(+()-(+())))-()", tree.GetFormForEdge(4));

			// Center: Vertex 2
			Assert.AreEqual("0(+()+(-())-(+()))", tree.GetNormalForm());
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
			var tree = new DirectedTree(edges);

			Assert.AreEqual("0(+(+()-())+(-()-()))", tree.GetFormForVertex(0));
			Assert.AreEqual("0(-()-()-(+(+()-())))", tree.GetFormForVertex(1));
			Assert.AreEqual("0(+()-()-(+(-()-())))", tree.GetFormForVertex(2));
			Assert.AreEqual("0(+(-()-(+(+()-()))))", tree.GetFormForVertex(3));
			Assert.AreEqual("0(+(-()-(+(+()-()))))", tree.GetFormForVertex(4));
			Assert.AreEqual("0(+(+()-(+(-()-()))))", tree.GetFormForVertex(5));
			Assert.AreEqual("0(-(-()-(+(-()-()))))", tree.GetFormForVertex(6));
		}

		[TestMethod]
		public void Parse_6_1()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (0, 2), (2, 3), (2, 4), (5, 4) }, DirectedTree.Parse("0(+()+(+()+(-())))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (1, 2), (2, 3), (2, 4), (5, 4) }, DirectedTree.Parse("0(-(+(+()+(-()))))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (0, 2), (3, 2), (4, 0), (4, 5) }, DirectedTree.Parse("0(+()+(-())-(+()))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (2, 1), (2, 3), (4, 2), (4, 5) }, DirectedTree.Parse("0(+(-(+()-(+()))))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (1, 2), (3, 2), (4, 1), (4, 5) }, DirectedTree.Parse("0(-(+(-())-(+())))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (2, 0), (2, 3), (4, 2), (4, 5) }, DirectedTree.Parse("0(-()-(+()-(+())))").edges);

			CollectionAssert.AreEqual(new[] { (1, 2), (2, 3), (2, 4), (5, 4), (1, 0) }, DirectedTree.Parse("+()-(+(+()+(-())))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (0, 2), (3, 2), (4, 5), (4, 0) }, DirectedTree.Parse("+(+()+(-()))-(+())").edges);
			CollectionAssert.AreEqual(new[] { (1, 2), (3, 2), (4, 1), (4, 5), (1, 0) }, DirectedTree.Parse("+()-(+(-())-(+()))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (2, 3), (4, 2), (4, 5), (2, 0) }, DirectedTree.Parse("+(-())-(+()-(+()))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (1, 2), (3, 1), (3, 4), (5, 0) }, DirectedTree.Parse("+(-(+()-(+())))-()").edges);
		}

		[TestMethod]
		public void Parse_7()
		{
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (3, 1), (0, 4), (5, 4), (6, 4) }, DirectedTree.Parse("0(+(+()-())+(-()-()))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (2, 0), (3, 0), (3, 4), (4, 5), (6, 4) }, DirectedTree.Parse("0(-()-()-(+(+()-())))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (2, 0), (3, 0), (3, 4), (5, 4), (6, 4) }, DirectedTree.Parse("0(+()-()-(+(-()-())))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (2, 1), (3, 1), (3, 4), (4, 5), (6, 4) }, DirectedTree.Parse("0(+(-()-(+(+()-()))))").edges);
			CollectionAssert.AreEqual(new[] { (0, 1), (1, 2), (3, 1), (3, 4), (5, 4), (6, 4) }, DirectedTree.Parse("0(+(+()-(+(-()-()))))").edges);
			CollectionAssert.AreEqual(new[] { (1, 0), (2, 1), (3, 1), (3, 4), (5, 4), (6, 4) }, DirectedTree.Parse("0(-(-()-(+(-()-()))))").edges);
		}
	}
}
