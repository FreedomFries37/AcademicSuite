using System.Windows;
using System.Windows.Input;
using AcademicApplication;
using Terminal.src;

namespace Terminal {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow{
		private src.ConsoleContent dc;

		private AcademicApp academicApp;

		public MainWindow() {
			InitializeComponent();
			DataContext = dc = new src.ConsoleContent();
			dc.inputTextBox = InputBlock;

			Loaded += Terminal_Loaded;
		}
		
		public MainWindow(AcademicApp academicApp) {
			InitializeComponent();
			DataContext = dc = new src.ConsoleContent(academicApp);
			
			dc.inputTextBox = InputBlock;

			this.academicApp = academicApp;
			Loaded += Terminal_Loaded;
		}


		void Terminal_Loaded(object sender, RoutedEventArgs e) {
			if (dc.ConsoleOutput.Count == 0) {
				if (academicApp == null) dc.addMessage(new Message("Loaded Terminal"));
				else dc.addMessage(new Message("Loaded Terminal for " + academicApp));
			}

			InputBlock.KeyDown += InputBlock_KeyDown;
			Overall.GotFocus += OverallOnFocus;
			InputBlock.Focus();
			
		}

		private void OverallOnFocus(object sender, RoutedEventArgs e) {
			InputBlock.Focus();
			//MessageBox.Show("down");
		}

		void InputBlock_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				dc.ConsoleInput = InputBlock.Text;
				dc.RunCommand();
				InputBlock.Focus();
				Viewer.ScrollToBottom();
				//InputBlock.Text = string.Empty;
			}
		}
		
		
	}
}