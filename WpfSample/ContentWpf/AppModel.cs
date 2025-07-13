using System.IO;

namespace ContentWpf
{
	public class AppModel
	{
		public bool IsForDesign { get; set; }

		public string ContentImagePath { get; } = @".\Images\slime.png";
		public string ContentImageFullPath => Path.GetFullPath(ContentImagePath);

		public string NoneImagePath { get; } = @".\Images\menfukuro.png";
		public string NoneImageFullPath => Path.GetFullPath(NoneImagePath);
		public byte[] NoneImageData => File.ReadAllBytes(NoneImagePath);
	}
}
