using System.Windows;
using AcademicApplication;
using Terminal.src;

namespace Calendar {
	public class Central : AcademicApp {
		private MainWindow window;

		public MainWindow Window {
			get => window;
			set => window = value;
		}

		
		protected override Status runAddedArguments(Command c) {
			switch (c.MainArgs) {
				case "add": {


					
					return Status.successStatus;
				}
				default:
					return Status.noCommand;
			}
		}

		protected override void exit() {
			Application.Current.Shutdown();
		}
	}
}