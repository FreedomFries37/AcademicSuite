using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Terminal;

namespace Calendar {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {

		private Central central;
		public MainWindow() {
			InitializeComponent();
			central = new Central();
			Loaded += Central_Loaded;
		}
	//


		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
			MessageBox.Show("Hello World");
		}

		private void Central_Loaded(object sender, RoutedEventArgs e) {
			TerminalWindow terminalWindow = new TerminalWindow(central);
			terminalWindow.Show();
		}

		private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
			Console.WriteLine("Sender: " + sender);
			Console.WriteLine("e: " + e);
		}


		private void exit_OnClick(object sender, RoutedEventArgs e) {
			MessageBox.Show("sender: " + sender);
		}

		private void terminal_OnClick(object sender, RoutedEventArgs e) {
			TerminalWindow terminalWindow = new TerminalWindow(central);
			terminalWindow.Show();
		}
	}
}