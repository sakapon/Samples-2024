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
				DataItems.ItemsSource = d;
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

		public static Obj[] ToDictionary(IDataObject data)
		{
			var l = new List<Obj>();
			foreach (var f in data.GetFormats())
			{
				var o = new Obj { Key = f };
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

	public class Obj
	{
		public string Key { get; set; }
		public object Value { get; set; }

		public string TypeName => Value?.GetType().FullName ?? "None";
		public string ValueString
		{
			get
			{
				if (Value is null) return "None";
				if (Value is string[] vs) return string.Join("\n", vs);
				return Value.ToString();
			}
		}
	}
}
