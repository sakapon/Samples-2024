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
		IIterable<T> Sort<TKey>(Func<T, TKey> func) => new SortIterable<T, TKey>(this, func);

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

	class SortIterable<T, TKey> : ArrayIterable<T>
	{
		public SortIterable(IIterable<T> source, Func<T, TKey> func) : base(Sort(source, func)) { }

		static T[] Sort(IIterable<T> source, Func<T, TKey> func)
		{
			var l = new List<T>();
			var lkey = new List<TKey>();
			while (source.MoveNext())
			{
				l.Add(source.Current);
				lkey.Add(func(source.Current));
			}

			var a = l.ToArray();
			var keys = lkey.ToArray();
			Array.Sort(keys, a);
			return a;
		}
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
		int i = -1;

		public ArrayIterable(T[] source)
		{
			this.source = source;
		}

		public T Current => source[i];
		public bool MoveNext() => ++i < source.Length;
	}

	public static class Iterable
	{
		public static IIterable<T> AsIterable<T>(this IEnumerable<T> source) => new EnumerableIterable<T>(source);
		public static IIterable<T> AsIterable<T>(this T[] source) => new ArrayIterable<T>(source);
	}
}
