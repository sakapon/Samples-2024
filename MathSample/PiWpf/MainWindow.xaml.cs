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

		decimal pi = 0;

		void Initialize()
		{
			Task.Run(() =>
			{
				var positive = true;
				for (int i = 1; ; i += 2, positive ^= true)
				{
					if (positive) pi += 4m / i;
					else pi -= 4m / i;
				}
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
	}
}
