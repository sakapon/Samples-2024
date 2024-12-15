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
		const int SyncInterval = 80;
		const int WheelUnit = 120;

		double fontSizeWheel = 24 * WheelUnit;
		PiCalculator model = new PiCalculator();

		public MainWindow()
		{
			InitializeComponent();

			RealNumber.MaxOffset = 5000;

			Loaded += (o, e) => Initialize();
			LeibnizButton.Click += (o, e) => Task.Run(() => model.ByLeibniz());
			MadhavaButton.Click += (o, e) => Task.Run(() => model.ByMadhava());
			RamanujanButton.Click += (o, e) => Task.Run(() => model.ByRamanujan());
			StopButton.Click += (o, e) => model.Stop();

			PiText.MouseWheel += (o, e) =>
			{
				fontSizeWheel += e.Delta;
				if (fontSizeWheel < 2 * WheelUnit) fontSizeWheel = 2 * WheelUnit;
				PiText.FontSize = fontSizeWheel / WheelUnit;
			};
		}

		void Initialize()
		{
			PiText.FontSize = fontSizeWheel / WheelUnit;

			var syncTimer = new System.Timers.Timer(SyncInterval);
			syncTimer.Elapsed += (o, e) =>
			{
				Dispatcher.InvokeAsync(() =>
				{
					LeibnizButton.IsEnabled = !model.IsOn;
					MadhavaButton.IsEnabled = !model.IsOn;
					RamanujanButton.IsEnabled = !model.IsOn;
					StopButton.IsEnabled = model.IsOn;
					PiText.Text = model.Pi.ToString();
				});
			};
			syncTimer.Start();
		}
	}
}
