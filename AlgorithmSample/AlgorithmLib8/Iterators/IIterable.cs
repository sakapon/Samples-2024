using System;
using System.Collections.Generic;

namespace AlgorithmLib8.Iterators
{
	public interface IIterable<T>
	{
		T Current { get; }
		bool MoveNext();

		IIterable<T> Filter(Func<T, bool> func) => new FilterIterable<T>(this, func);
		IIterable<TResult> Map<TResult>(Func<T, TResult> func) => new MapIterable<T, TResult>(this, func);

		T[] ToArray()
		{
			var l = new List<T>();
			while (MoveNext()) l.Add(Current);
			return l.ToArray();
		}
	}

	class FilterIterable<T> : IIterable<T>
	{
		readonly IIterable<T> source;
		readonly Func<T, bool> func;

		public FilterIterable(IIterable<T> source, Func<T, bool> func)
		{
			this.source = source;
			this.func = func;
		}

		public T Current => source.Current;
		public bool MoveNext()
		{
			while (source.MoveNext()) if (func(Current)) return true;
			return false;
		}
	}

	class MapIterable<T, TResult> : IIterable<TResult>
	{
		readonly IIterable<T> source;
		readonly Func<T, TResult> func;

		public MapIterable(IIterable<T> source, Func<T, TResult> func)
		{
			this.source = source;
			this.func = func;
		}

		public TResult Current => func(source.Current);
		public bool MoveNext() => source.MoveNext();
	}

	class EnumerableIterable<T> : IIterable<T>
	{
		readonly IEnumerator<T> source;

		public EnumerableIterable(IEnumerable<T> source)
		{
			this.source = source.GetEnumerator();
		}

		public T Current => source.Current;
		public bool MoveNext() => source.MoveNext();
	}

	class ArrayIterable<T> : IIterable<T>
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
