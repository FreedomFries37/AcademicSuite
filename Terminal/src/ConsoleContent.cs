using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using AcademicApplication;

namespace Terminal.src {
	public class ConsoleContent : CommandToAppInteraction{
		private CommandToAppInteraction interaction;
		string consoleInput = String.Empty;
		static ObservableCollection<Message> consoleOutput = new ObservableCollection<Message>();
		
		static Dictionary<string, string> variables = new Dictionary<string, string>();
		public const int MaxDepth = 15;
		
		public ConsoleContent(CommandToAppInteraction interaction) {
			this.interaction = interaction;
		}

		public ConsoleContent() {
			
		}

		public CommandToAppInteraction Interaction {
			get => interaction;
			set => interaction = value;
		}

		public string ConsoleInput
		{
			get
			{
				return consoleInput;
			}
			set
			{
				consoleInput = value;
				OnPropertyChanged("ConsoleInput");
			}
		}

		public TextBox inputTextBox { set; get; }

		public ObservableCollection<Message> ConsoleOutput
		{
			get
			{
				return consoleOutput;
			}
			set
			{
				consoleOutput = value;
				OnPropertyChanged("ConsoleOutput");
			}
		}

		public void RunCommand() {
			string mod = ConsoleInput;
			bool hide = false;
			if (mod.StartsWith("@")) {
				hide = true;
				mod = mod.Substring(1);
			}

			if (!mod.StartsWith("define")) {
				int depth = 0;
				while (depth < MaxDepth && mod.Contains("$")) {
					foreach (KeyValuePair<string, string> keyValuePair in variables) {
						string key = keyValuePair.Key;
						string val = keyValuePair.Value;

						mod = mod.Replace("$" + key, val);
					}

					depth++;
				}
			}

			if(!hide) ConsoleOutput.Add(new Message(mod));
			if (mod.Length > 0) {
				Command a = new Command(mod);


				Status s = runArguments(a);
				if (s == null || s.Equals(Status.noCommand)) s = interaction.runArguments(a);
				
				if (s != null) {
					if (!s.Success) {
						if(s.Equals(Status.noCommand))
							addErrorMessage("Command " + a.MainArgs + " not found!");
						else
							addErrorMessage(s.Message);
					}
				}
			}

			// do your stuff here.
			ConsoleInput = String.Empty;
			inputTextBox.Text = String.Empty;
		}

		protected override Status runAddedArguments(Command c) {
			switch (c.MainArgs) {
				case "echo": {
					String created = "";
					bool first = true;
					foreach (string d in c.Rest) {
						if (!first) {
							created += " ";
						} else first = false;
						created += d;
					}
					addMessage(created);
					return Status.successStatus;
				}
				case "shutdown": {
					Application.Current.Shutdown();
					return null;
				}
				case "define": {

					if (!variables.ContainsKey(c.Rest[0])) {
						variables.Add(c.Rest[0], c.Rest[1]);
					} else {
						variables[c.Rest[0]] = c.Rest[1];
					}

					return Status.successStatus;
				}
				case "listvars": {
					foreach (var keyValuePair in variables) {
						addMessage($"{keyValuePair.Key} => {keyValuePair.Value}");
					}

					return Status.successStatus;
				}

				default:
						return null;
				}
		}

		protected override void exit() {
			foreach (var mainWindow in Application.Current.Windows.OfType<Terminal.MainWindow>()) {
				if(mainWindow.DataContext.Equals(this))
					mainWindow.Close();
			}
		}

		public void addMessage(Message message) {
			message.UserGenerated = false;
			ConsoleOutput.Add(message);
		}
		public void addMessage(string message) {
			addMessage(new Message(message));
		}
		public void addErrorMessage(string message) {
			addMessage(new Error(message));
		}
		

		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged(string propertyName)
		{
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		
		
		
	}
}