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
		public MainWindow()
		{
			InitializeComponent();

			Drop += (o, e) =>
			{
				var d = ToDictionary(e.Data);
				var vm = (MainViewModel)DataContext;
				vm.DataItems.Value = d;
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
			var l = new List<DataItem>();
			foreach (var f in data.GetFormats())
			{
				var o = new DataItem { Key = f };
				try
				{
					o.Value = data.GetData(f);
				}
				catch (Exception)
				{
				}
				l.Add(o);
			}
			return l.ToArray();
		}
	}
}
