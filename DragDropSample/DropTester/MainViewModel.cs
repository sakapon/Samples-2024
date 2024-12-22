using Reactive.Bindings;

namespace DropTester
{
	public class MainViewModel
	{
		public ReactiveProperty<DataItem[]> DataItems { get; } = new ReactiveProperty<DataItem[]>();
	}

	public class DataItem
	{
		public string Key { get; set; }
		public object Value { get; set; }

		public string TypeName => Value?.GetType().FullName ?? "None";
		public string ValueString => Value switch
		{
			null => "None",
			string[] vs => string.Join("\n", vs),
			_ => Value.ToString(),
		};
	}
}
