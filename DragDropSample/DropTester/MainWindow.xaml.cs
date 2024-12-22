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

namespace DropTester
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly MainViewModel vm;

		public MainWindow()
		{
			InitializeComponent();

			vm = (MainViewModel)DataContext;

			Drop += (o, e) =>
			{
				vm.DataItems.Value = ToDictionary(e.Data);
				vm.AllowedEffects.Value = e.AllowedEffects.ToString();
				vm.KeyStates.Value = e.KeyStates.ToString();
			};

			Rect1.DragOver += (o, e) =>
			{
				// Effects プロパティで動作を指定します。
				//e.Effects = DragDropEffects.None;
			};
			Rect1.Drop += (o, e) =>
			{
			};
		}

		public static DataItem[] ToDictionary(IDataObject data)
		{
			return Array.ConvertAll(data.GetFormats(), f =>
			{
				try
				{
					return new DataItem(f, data.GetData(f));
				}
				catch (Exception)
				{
					return new DataItem(f, null);
				}
			});
		}
	}
}
