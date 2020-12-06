using System;
using System.Collections.Generic;

namespace Quantum.School.Api.Messages
{
	public class ClassScheduleResponse
	{
		public Guid Id { get; set; }

		public string Location { get; set; }

		public SubjectResponse Subject { get; set; }

		public TeacherResponse Teacher { get; set; }

		public IEnumerable<StudentResponse> Students { get; set; }
	}
}
