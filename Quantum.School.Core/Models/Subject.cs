using System;
using System.Collections.Generic;

namespace Quantum.School.Core.Models
{
	public class Subject : Entity<Guid>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public virtual ICollection<Teacher> Teachers { get; set; }

		public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
	}
}
