using System.Windows.Media;

namespace Terminal.src {
	public class Error : Message{
		
		private static Brush defaultError = Brushes.Red;
		
		public Error(string str) : base(str, defaultError) { }
	}
}