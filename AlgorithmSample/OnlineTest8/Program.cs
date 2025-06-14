using System;
using System.Linq;
using AlgorithmLib8.Iterators;

namespace OnlineTest8
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			IterableTest();
		}

		static void IterableTest()
		{
			var result = Enumerable.Range(0, 30).AsIterable()
				.Filter(x => x % 3 == 0)
				.Map(x => x * 2)
				.Sort(x => x.ToString())
				.ToArray();
			Array.ForEach(result, Console.WriteLine);
		}
	}
}
