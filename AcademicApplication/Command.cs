using System;
using System.Text.RegularExpressions;

namespace Terminal.src {
	public class Command {
		private string[] args;

		public string MainArgs=> args[0];

		public string[] Rest {
			get {
				if (args.Length <= 1) return new string[0];
				string[] output = new string[args.Length-1];
				Array.Copy(args, 1, output, 0, args.Length-1);
				return output;
			}
		}

		public Command(string[] args) {
			this.args = args;
		}
		
		public Command(string args) {
			this.args = Regex.Split(args, "\\s+");
		}

		public string[] Args => args;
	}
}