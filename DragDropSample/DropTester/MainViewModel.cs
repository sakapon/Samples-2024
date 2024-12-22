using System.IO;
using System.Text;
using Reactive.Bindings;

namespace DropTester
{
	public class MainViewModel
	{
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
