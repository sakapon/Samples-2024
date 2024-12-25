using Reactive.Bindings;
using DDE = System.Windows.DragDropEffects;

namespace DragTester
{
	public class MainViewModel
	{
		public DragType[] DragTypes { get; } = Enum.GetValues<DragType>();
		public ReactiveProperty<DragType> SelectedDragType { get; } = new ReactiveProperty<DragType>();
		public DDE[] DragDropEffects { get; } = new[] { DDE.Copy, DDE.Move, DDE.Link, DDE.Scroll };
		public ReactiveProperty<string> Effects { get; } = new ReactiveProperty<string>();
	}

	public enum DragType
	{
		File,
		Files,
		Text,
		Tuple,
		Custom,
	}
}
