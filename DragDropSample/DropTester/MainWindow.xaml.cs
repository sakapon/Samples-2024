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

			// 上の階層のコントロールから順にイベントが発生します。
			// EventArgs オブジェクトは共有されます。
			DropText.DragOver += (o, e) =>
			{
				vm.DataItems.Value = null;
				vm.AllowedEffects.Value = e.AllowedEffects.ToString();
				vm.KeyStates.Value = e.KeyStates.ToString();

				// Effects プロパティに処理可能な動作を指定します。
				// AllowedEffects と Effects が排他的となる場合、Drop イベントが抑制されます。
				e.Effects &= DDEList.SelectedItems.Cast<DragDropEffects>().Aggregate(DragDropEffects.None, (x, y) => x | y);
				e.Handled = true;
			};

			DropText.Drop += (o, e) =>
			{
				vm.DataItems.Value = ToDataItems(e.Data);
				vm.AllowedEffects.Value = e.AllowedEffects.ToString();
				vm.KeyStates.Value = e.KeyStates.ToString();

				// AllowedEffects, KeyStates の値をもとに、ドロップされたときの処理を決定します。

				// ドラッグ元に返す値。
				// Move が含まれる場合、ドラッグ元で対象が削除されることがあります。
				e.Effects &= DDEList.SelectedItems.Cast<DragDropEffects>().Aggregate(DragDropEffects.None, (x, y) => x | y);
				e.Handled = true;
			};

			DragOver += (o, e) =>
			{
				vm.DataItems.Value = null;
				vm.AllowedEffects.Value = e.AllowedEffects.ToString();
				vm.KeyStates.Value = e.KeyStates.ToString();

				// Effects プロパティに処理可能な動作を指定します。
				// AllowedEffects と Effects が排他的となる場合、Drop イベントが抑制されます。
				//e.Effects = DragDropEffects.Copy;
				e.Handled = true;
			};

			Drop += (o, e) =>
			{
				vm.DataItems.Value = ToDataItems(e.Data);
				vm.AllowedEffects.Value = e.AllowedEffects.ToString();
				vm.KeyStates.Value = e.KeyStates.ToString();

				// AllowedEffects, KeyStates の値をもとに、ドロップされたときの処理を決定します。

				// ドラッグ元に返す値。
				// Move が含まれる場合、ドラッグ元で対象が削除されることがあります。
				//e.Effects = DragDropEffects.Copy;
				e.Handled = true;
			};

			// 文字列は e.Data.GetData(DataFormats.UnicodeText) で取得します。
		}

		public static DataItem[] ToDataItems(IDataObject data)
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
