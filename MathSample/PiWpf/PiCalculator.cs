using System.Numerics;

namespace PiWpf
{
	public class PiCalculator
	{
		public bool IsOn;
		public decimal Pi_d;
		public RealNumber Pi;

		public void Stop()
		{
			IsOn = false;
		}

		public void ByLeibniz_d()
		{
			IsOn = true;
			Pi_d = 0;
			var pos = true;
			for (long i = 1; IsOn; i += 2, pos ^= true)
			{
				var d = 4m / i;
				if (pos) Pi_d += d;
				else Pi_d -= d;
			}
		}

		public void ByLeibniz()
		{
			IsOn = true;
			Pi = 0;
			RealNumber n4 = 4;
			var pos = true;
			for (long i = 1; IsOn; i += 2, pos ^= true)
			{
				var d = n4 / i;
				if (pos) Pi += d;
				else Pi -= d;
			}
		}

		public void ByMadhava_d()
		{
			IsOn = true;
			var r12 = (decimal)Math.Sqrt(12);
			Pi_d = r12;
			var p = 1m;
			for (long i = 3; IsOn; i += 2)
			{
				p *= -3;
				Pi_d += r12 / (i * p);
			}
		}

		public void ByMadhava()
		{
			IsOn = true;
			var r12 = RealNumber.Sqrt(12);
			Pi = r12;
			RealNumber p = 1;
			for (long i = 3; IsOn; i += 2)
			{
				p *= -3;
				Pi += r12 / (i * p);
			}
		}

		public void ByRamanujan()
		{
			IsOn = true;
			RealNumber c = RealNumber.Sqrt(2) * (99 * 99) / 4;
			BigInteger a = 1103, p = 1, d = 1;
			RealNumber s = a;
			Pi = c / s;
			for (long i = 1; IsOn; ++i)
			{
				a += 26390;
				p = p * (4 * i - 3) * (4 * i - 2) * (4 * i - 1) * (4 * i);
				d = d * (256L * 99 * 99 * 99 * 99) * i * i * i * i;
				s += (RealNumber)(a * p) / d;
				Pi = c / s;
			}
		}

		public void ByRamanujan2()
		{
			IsOn = true;
			RealNumber c = 4;
			BigInteger a = 1123, p = 1, d = 882;
			var s = (RealNumber)a / d;
			Pi = c / s;
			for (long i = 1; IsOn; ++i)
			{
				a += 21460;
				p = p * -(4 * i - 3) * (4 * i - 2) * (4 * i - 1) * (4 * i);
				d = d * (882 * 882 * 256) * i * i * i * i;
				s += (RealNumber)(a * p) / d;
				Pi = c / s;
			}
		}
	}
}
