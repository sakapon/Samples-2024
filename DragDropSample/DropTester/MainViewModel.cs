using Reactive.Bindings;

namespace DropTester
{
	public class MainViewModel
	{
		public ReactiveProperty<Obj[]> DataItems { get; } = new ReactiveProperty<Obj[]>();
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
