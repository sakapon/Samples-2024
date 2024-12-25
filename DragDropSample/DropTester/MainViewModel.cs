using System.IO;
using System.Text;
using Reactive.Bindings;
using DDE = System.Windows.DragDropEffects;

namespace DropTester
{
	public class MainViewModel
	{
		public DDE[] DragDropEffects { get; } = new[] { DDE.Copy, DDE.Move, DDE.Link, DDE.Scroll };
		public ReactiveProperty<DataItem[]> DataItems { get; } = new ReactiveProperty<DataItem[]>();
		public ReactiveProperty<string> AllowedEffects { get; } = new ReactiveProperty<string>();
		public ReactiveProperty<string> KeyStates { get; } = new ReactiveProperty<string>();
	}

	public record DataItem(string Key, object Value)
	{
		public string TypeName => Value?.GetType().FullName ?? "(None)";
		public string ValueString => Value switch
		{
			null => "(None)",
			string[] vs => string.Join("\n", vs),
			MemoryStream stream => Encoding.UTF8.GetString(stream.ToArray()),
			_ => Value.ToString(),
		};
	}
}
