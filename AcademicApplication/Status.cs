using System.Collections.Generic;

namespace AcademicApplication {
	public class Status {
		private bool success;
		private string message;
		public Status(bool success, string message) {
			this.success = success;
			this.message = message;
		}

		public bool Success => success;

		public string Message => message;
		
		public static Status successStatus = new Status(true, "");
		public static Status failStatus(string message) => new Status(false, message);
		public static Status failStatus() => new Status(false, "");
		
		public static Status noCommand = new Status(false, "no command");

		protected bool Equals(Status other) {
			return success == other.success && string.Equals(message, other.message);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Status) obj);
		}

		public override int GetHashCode() {
			unchecked {
				return (success.GetHashCode() * 397) ^ (message != null ? message.GetHashCode() : 0);
			}
		}
	}
}