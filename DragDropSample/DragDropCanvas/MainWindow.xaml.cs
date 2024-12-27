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

namespace DragDropCanvas
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var circle = CreateCircle();
			Canvas.SetLeft(circle, 100);
			Canvas.SetTop(circle, 100);
			TheCanvas.Children.Add(circle);

			// 完全な透明だとドロップ可能にならないため、Canvas に Background を指定します。
		}

		void TheCanvas_DragOver(object sender, DragEventArgs e)
		{
			e.Effects = e.Data.GetDataPresent("Circle") ? DragDropEffects.Move : DragDropEffects.None;
			e.Handled = true;
		}

		void TheCanvas_Drop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent("Circle"))
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				return;
			}

			var offset = (Point)e.Data.GetData("Circle");
			var position = e.GetPosition(TheCanvas) - offset;

			var circle = CreateCircle();
			Canvas.SetLeft(circle, position.X);
			Canvas.SetTop(circle, position.Y);
			TheCanvas.Children.Add(circle);

			e.Effects = DragDropEffects.Move;
			e.Handled = true;
		}

		void Circle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var circle = (Ellipse)sender;
			var offset = Mouse.GetPosition(circle);

			// 現在のアプリ内であれば、カスタム型でも可能。
			var data = new DataObject("Circle", offset);
			var result = DragDrop.DoDragDrop(circle, data, DragDropEffects.Move);
			e.Handled = true;

			if (result == DragDropEffects.Move)
				TheCanvas.Children.Remove(circle);
		}

		Ellipse CreateCircle()
		{
			var circle = new Ellipse
			{
				Width = 100,
				Height = 100,
				Fill = new SolidColorBrush(Colors.Orange),
			};
			circle.MouseLeftButtonDown += Circle_MouseLeftButtonDown;
			return circle;
		}
	}
}
