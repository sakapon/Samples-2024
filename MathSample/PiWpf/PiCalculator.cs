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
		}
	}
}
