using System;

namespace Quantum.School.Core.Models
{
	public sealed class NameTitle : Enumeration<string>
	{
		public static readonly NameTitle Mr   = new("Mr",   "Mr");
		public static readonly NameTitle Ms   = new("Ms",   "Ms");
		public static readonly NameTitle Dr   = new("Dr",   "Dr");
		public static readonly NameTitle Engr = new("Engr", "Engr");
		public static readonly NameTitle None = new("", "");

		public NameTitle() { }

		private NameTitle(string id, string name) : base(id, name) { }

		public static NameTitle FromId(string id)
		{
			return Enumeration<string>.FromId<NameTitle>(id);
		}

		public static NameTitle FromName(string name)
		{
			return Enumeration<string>.FromName<NameTitle>(name);
		}
	}
}
