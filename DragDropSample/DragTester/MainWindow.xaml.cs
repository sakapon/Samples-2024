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

			DragFileText.MouseLeftButtonDown += (o, e) =>
			{
				var files = new[] { CreateTempFile() };
				var data = new DataObject(DataFormats.FileDrop, files);
				var result = DragDrop.DoDragDrop((DependencyObject)o, data, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
				vm.Effects.Value = result.ToString();
				Array.ForEach(files, File.Delete);
			};

			DragFileText.MouseRightButtonDown += (o, e) =>
			{
				var files = new[] { CreateTempFile() };
				var data = new DataObject(DataFormats.FileDrop, files);
				var result = DragDrop.DoDragDrop((DependencyObject)o, data, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
				vm.Effects.Value = result.ToString();
				Array.ForEach(files, File.Delete);
			};
		}

		static string CreateTempFile()
		{
			var fileName = Path.GetRandomFileName();
			var filePath = Path.GetFullPath(Path.Combine(DirName, fileName));
			File.WriteAllText(filePath, "");
			return filePath;
		}
	}
}
