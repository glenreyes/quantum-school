using System;
using System.Collections.Generic;

namespace Quantum.School.Core.Models
{
	//TODO: Implement Employee ID instead of generated GUID
	public class Teacher : Person
	{
		public string TitleId { get; set; }
		public virtual NameTitle Title
		{
			get { return NameTitle.FromId(TitleId); }
			set { TitleId = value.Id; }
		}

		public virtual ICollection<Subject> Subjects { get; set; }

		public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
	}
}
