using System;
using System.Numerics;

#nullable disable
namespace PiWpf
{
	public struct RealNumber : IEquatable<RealNumber>
	{
		public static int MaxOffset = 1000;

		public BigInteger Value;
		public int Offset;

		public RealNumber(BigInteger value, int offset)
		{
			Value = value;
			Offset = offset;
		}

		// 負値には非対応
		public override readonly string ToString()
		{
			var s = Value.ToString();
			if (Offset == 0) return s;
			s = s.PadLeft(Offset + 1, '0');
			return $"{s[..^Offset]}.{s[^Offset..]}";
		}

		public readonly bool Equals(RealNumber other) => Value == other.Value && Offset == other.Offset;
		public static bool operator ==(RealNumber v1, RealNumber v2) => v1.Equals(v2);
		public static bool operator !=(RealNumber v1, RealNumber v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is RealNumber v && Equals(v);
		public override readonly int GetHashCode() => (Value, Offset).GetHashCode();

		public static implicit operator RealNumber(int value) => new RealNumber(value, 0);
		public static implicit operator RealNumber(long value) => new RealNumber(value, 0);
		public static implicit operator RealNumber(BigInteger value) => new RealNumber(value, 0);

		public static RealNumber operator +(RealNumber v1, RealNumber v2)
		{
			if (v1.Offset < v2.Offset) (v1, v2) = (v2, v1);
			var d = v1.Offset - v2.Offset;
			return new RealNumber(v1.Value + v2.Value * BigInteger.Pow(10, d), v1.Offset);
		}

		public static RealNumber operator -(RealNumber v1, RealNumber v2)
		{
			return v1 + new RealNumber(-v2.Value, v2.Offset);
		}

		public static RealNumber operator *(RealNumber v1, RealNumber v2)
		{
			var v = v1.Value * v2.Value;
			var o = v1.Offset + v2.Offset;
			Floor(ref v, ref o);
			return new RealNumber(v, o);
		}

		public static RealNumber operator /(RealNumber v1, RealNumber v2)
		{
			var v = v1.Value;
			var o = v1.Offset;
			Expand(ref v, ref o);
			v /= v2.Value;
			o -= v2.Offset;
			Floor(ref v, ref o);
			return new RealNumber(v, o);
		}

		static void Floor(ref BigInteger v, ref int o)
		{
			if (o <= MaxOffset) return;
			var d = o - MaxOffset;
			v /= BigInteger.Pow(10, d);
			o = MaxOffset;
		}

		static void Expand(ref BigInteger v, ref int o)
		{
			var d = (MaxOffset << 1) - o;
			v *= BigInteger.Pow(10, d);
			o = MaxOffset << 1;
		}

		// ニュートン法
		public static RealNumber Sqrt(RealNumber v)
		{
			RealNumber x = 1;
			for (var i = 0; i < 100; i++)
			{
				var temp = (x + v / x) / 2;
				if (x == temp) break;
				x = temp;
			}
			return x;
		}
	}
}
