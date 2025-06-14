using System;
using System.Collections.Generic;

namespace AlgorithmLib8.Iterators
{
	public interface IIterable<T>
	{
		T Current { get; }
		bool MoveNext();

		T[] ToArray()
		{
			var l = new List<T>();
			while (MoveNext()) l.Add(Current);
			return l.ToArray();
		}
	}

	public class EnumerableIterable<T> : IIterable<T>
	{
		readonly IEnumerator<T> source;

		public EnumerableIterable(IEnumerable<T> source)
		{
			this.source = source.GetEnumerator();
		}

		public T Current => source.Current;
		public bool MoveNext() => source.MoveNext();
	}

	public class ArrayIterable<T> : IIterable<T>
	{
		readonly T[] source;
		int i;

		public ArrayIterable(T[] source)
		{
			this.source = source;
		}

		public T Current => source[i];
		public bool MoveNext() => i++ < source.Length;
	}

	public static class Iterable
	{
		public static IIterable<T> AsIterable<T>(this IEnumerable<T> source) => new EnumerableIterable<T>(source);
		public static IIterable<T> AsIterable<T>(this T[] source) => new ArrayIterable<T>(source);
	}
}
