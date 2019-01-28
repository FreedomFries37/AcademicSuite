using System;
using System.Windows.Media;

namespace Terminal.src {
	public class Message {
		
		public static Brush defaultColor = Brushes.White;
		public static Message empty = new Message("", defaultColor);

		private string str;
		private Brush color;
		private bool userGenerated = true;
		private DateTime timeCreated = DateTime.Now;

		public bool UserGenerated {
			get => userGenerated;
			set => userGenerated = value;
		}

		public Message(string str, Brush color) {
			this.str = str;
			this.color = color;
		}

		public Message(string str) {
			this.str = str;
			color = defaultColor;
		}

		public string Str => str;

		public Brush Color => color;

		public DateTime TimeCreated => timeCreated; //
	}
}