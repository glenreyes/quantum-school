using System;
using System.Collections.Generic;

namespace Quantum.School.Core.Models
{
	public class Person : Entity<Guid>
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public DateTime? BirthDate { get; set; }

		//TODO: This is awkward. Find a better way of mapping Enumeration type to an entity
		public string GenderId { get; set; }
		public virtual Gender Gender
		{
			get { return Gender.FromId(GenderId); }
			set { GenderId = value.Id; }
		}
	}
}
