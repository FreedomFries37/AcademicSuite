using Terminal.src;

namespace AcademicApplication.Properties {
	public class CommandTemplate {
		private string mainArg;
		private int numArgs;
		private ArgCheckType argCheckType;

		public CommandTemplate(string mainArg, int numArgs = 0, ArgCheckType argCheckType = ArgCheckType.MIN) {
			this.mainArg = mainArg;
			this.numArgs = numArgs;
			this.argCheckType = argCheckType;
		}


		public bool matches(Command command) {
			if (command == null) return false;
			if (command.MainArgs != mainArg) return false;
			if (!matchesArgCheckType(command)) return false;

			return true;
		}

		private bool matchesArgCheckType(Command c) {
			switch (argCheckType) {
				case ArgCheckType.MAX: return c.Rest.Length <= numArgs;
				case ArgCheckType.MIN: return c.Rest.Length >= numArgs;
				case ArgCheckType.EQUAL: return c.Rest.Length == numArgs;
			}

			return false;
		}
	}

	public enum ArgCheckType {
		MIN, MAX, EQUAL
	}
}