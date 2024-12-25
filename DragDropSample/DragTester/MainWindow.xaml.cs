﻿using System.IO;
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

			DragTextText.MouseLeftButtonDown += (o, e) =>
			{
				var data = CreateDataObject(vm.SelectedDragType.Value);
				var effects = DDEList.SelectedItems.Cast<DragDropEffects>().Aggregate(DragDropEffects.None, (x, y) => x | y);
				var result = DragDrop.DoDragDrop((DependencyObject)o, data, effects);
				vm.Effects.Value = result.ToString();
				e.Handled = true;
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
					break;
				case DragType.Files:
					break;
				case DragType.Text:
					return new DataObject(DataFormats.UnicodeText, "あいうえお");
				case DragType.Tuple:
					break;
				case DragType.Custom:
					break;
				default:
					throw new InvalidOperationException();
			}
			throw new NotImplementedException();
		}
	}
}
