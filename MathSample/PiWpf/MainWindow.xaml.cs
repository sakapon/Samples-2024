using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PiWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		const int TextInterval = 80;

		public MainWindow()
		{
			InitializeComponent();

			RealNumber.MaxOffset = 5000;

			Loaded += (o, e) => Initialize();
		}

		void Initialize()
		{
			Task.Run(() =>
			{
				//StartLeibniz();
				StartMadhava();
			});

			var piTimer = new System.Timers.Timer(TextInterval);
			piTimer.Elapsed += (o, e) =>
			{
				Dispatcher.InvokeAsync(() =>
				{
					PiText.Text = pi.ToString();
				});
			};
			piTimer.Start();
		}

		decimal pi_d;
		RealNumber pi;

		void StartLeibniz_d()
		{
			pi_d = 0;
			var pos = true;
			for (int i = 1; ; i += 2, pos ^= true)
			{
				var d = 4m / i;
				if (pos) pi_d += d;
				else pi_d -= d;
			}
		}

		void StartLeibniz()
		{
			pi = 0;
			var n4 = (RealNumber)4;
			var pos = true;
			for (int i = 1; ; i += 2, pos ^= true)
			{
				var d = n4 / i;
				if (pos) pi += d;
				else pi -= d;
			}
		}

		void StartMadhava_d()
		{
			var r12 = (decimal)Math.Sqrt(12);
			pi_d = r12;
			var p = 1m;
			for (int i = 3; ; i += 2)
			{
				p *= -3;
				pi_d += r12 / (i * p);
			}
		}

		void StartMadhava()
		{
			var r12 = RealNumber.Sqrt(12);
			pi = r12;
			RealNumber p = 1;
			for (int i = 3; ; i += 2)
			{
				p *= -3;
				pi += r12 / (i * p);
			}
		}
	}
}
