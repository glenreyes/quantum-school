using System;
using System.Collections.Generic;

namespace Quantum.School.Core.Models
{
	public class ClassSchedule : Entity<Guid>
	{
		public virtual Subject Subject { get; set; }

		public virtual Teacher Teacher { get; set; }

		public string Location { get; set; }

		public virtual ICollection<Student> Students { get; set; }
	}
}
