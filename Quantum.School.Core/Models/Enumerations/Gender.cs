using System;

namespace Quantum.School.Core.Models
{
	public sealed class Gender : Enumeration<string>
	{
		public static readonly Gender Male         = new ("M", "Male");
		public static readonly Gender Female       = new ("F", "Female");
		public static readonly Gender NotSpecified = new ("",  "Not Specified");

		public Gender() { }

		private Gender(string id, string name) : base(id, name) { }

		public static Gender FromId(string id)
		{
			return Enumeration<string>.FromId<Gender>(id);
		}

		public static Gender FromName(string name)
		{
			return Enumeration<string>.FromName<Gender>(name);
		}
	}
}
