using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DragTester
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		const string DirName = "temp";
		readonly MainViewModel vm;

		public MainWindow()
		{
			InitializeComponent();

			vm = (MainViewModel)DataContext;

			Directory.CreateDirectory(DirName);

			// 上の階層のコントロールから順にイベントが発生します。
			// EventArgs オブジェクトは共有されます。
			DragText.MouseLeftButtonDown += (o, e) =>
			{
				var data = CreateDataObject(vm.SelectedDragType.Value);
				var effects = DDEList.SelectedItems.Cast<DragDropEffects>().Aggregate(DragDropEffects.None, (x, y) => x | y);
				var result = DragDrop.DoDragDrop((DependencyObject)o, data, effects);
				vm.Effects.Value = result.ToString();
				e.Handled = true;

				if (data.ContainsFileDropList() && (result & DragDropEffects.Link) == DragDropEffects.None) Array.ForEach(data.GetFileDropList().Cast<string>().ToArray(), File.Delete);
			};

			DragText.MouseRightButtonDown += (o, e) =>
			{
				var data = CreateDataObject(vm.SelectedDragType.Value);
				var effects = DDEList.SelectedItems.Cast<DragDropEffects>().Aggregate(DragDropEffects.None, (x, y) => x | y);
				var result = DragDrop.DoDragDrop((DependencyObject)o, data, effects);
				vm.Effects.Value = result.ToString();
				e.Handled = true;

				if (data.ContainsFileDropList() && (result & DragDropEffects.Link) == DragDropEffects.None) Array.ForEach(data.GetFileDropList().Cast<string>().ToArray(), File.Delete);
			};

			MouseLeftButtonDown += (o, e) =>
			{
				var data = new DataObject(DataFormats.UnicodeText, "Drag Tester");
				var result = DragDrop.DoDragDrop((DependencyObject)o, data, DragDropEffects.Copy | DragDropEffects.Move);
				vm.Effects.Value = result.ToString();
				e.Handled = true;
			};
		}

		static string CreateTempFile()
		{
			var fileName = Path.GetRandomFileName();
			var filePath = Path.GetFullPath(Path.Combine(DirName, fileName));
			File.WriteAllText(filePath, "");
			return filePath;
		}

		static DataObject CreateDataObject(DragType type)
		{
			switch (type)
			{
				case DragType.File:
					return new DataObject(DataFormats.FileDrop, new[] { CreateTempFile() });
				case DragType.Files:
					return new DataObject(DataFormats.FileDrop, new[] { CreateTempFile(), CreateTempFile() });
				case DragType.Text:
					return new DataObject(DataFormats.UnicodeText, "あいうえお");
				case DragType.Tuple:
					// 復元可能な型。
					return new DataObject(DataFormats.Serializable, ("あいうえお", 123));
				case DragType.Custom:
					// 任意のキーを利用可能。
					return new DataObject("DragTester", new MyClass());
				default:
					throw new InvalidOperationException();
			}
		}
	}

	public class MyClass
	{
	}
}
