using System;
using System.Numerics;

namespace PiWpf
{
	public struct RealNumber
	{
		public BigInteger Value;
		public int Offset;

		public RealNumber(BigInteger value, int offset)
		{
			Value = value;
			Offset = offset;
		}

		// 負値には非対応
		public readonly override string ToString()
		{
			var s = Value.ToString();
			if (Offset == 0) return s;
			s = s.PadLeft(Offset + 1, '0');
			return $"{s[..^Offset]}.{s[^Offset..]}";
		}

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
	}
}
