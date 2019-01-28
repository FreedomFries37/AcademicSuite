using System;
using AcademicApplication;
using AcademicApplication.Properties;

namespace Terminal.src {
	public abstract class CommandToAppInteraction {
		

		protected abstract Status runAddedArguments(Command c);

		protected abstract void exit();
		
		protected CommandTemplate exitTemplate = new CommandTemplate("exit", 0, ArgCheckType.EQUAL);

		public Status runArguments(Command c) {
			Status s = runAddedArguments(c);
			if (s != null && !s.Equals(Status.noCommand)) return s;
			switch (c.MainArgs) {
				case "exit": {
					if(!exitTemplate.matches(c)) return Status.failStatus("Too Many Arguments");
					exit();
					return Status.successStatus;
				}
				
				default:
					return Status.noCommand;
			}
		}
	}
}