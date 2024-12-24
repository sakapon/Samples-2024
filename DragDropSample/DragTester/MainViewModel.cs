using Reactive.Bindings;

namespace DragTester
{
	public class MainViewModel
	{
		public ReactiveProperty<string> Effects { get; } = new ReactiveProperty<string>();
	}
}
