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
		public MainWindow()
		{
			InitializeComponent();

			Loaded += (o, e) => Initialize();
		}

		void Initialize()
		{
			Task.Run(() =>
			{
				StartMadhava();
			});

			var piTimer = new System.Timers.Timer(50);
			piTimer.Elapsed += (o, e) =>
			{
				Dispatcher.InvokeAsync(() =>
				{
					PiText.Text = pi.ToString();
				});
			};
			piTimer.Start();
		}

		decimal pi = 0;

		void StartLeibniz()
		{
			pi = 0;
			var pos = true;
			for (int i = 1; ; i += 2, pos ^= true)
			{
				var d = 4m / i;
				if (pos) pi += d;
				else pi -= d;
			}
		}

		void StartMadhava()
		{
			var r12 = (decimal)Math.Sqrt(12);
			pi = r12;
			var p = 1m;
			for (int i = 3; ; i += 2)
			{
				p *= -3;
				pi += r12 / (i * p);
			}
		}
	}
}
