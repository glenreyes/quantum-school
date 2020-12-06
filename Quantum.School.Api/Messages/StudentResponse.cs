using System;
using System.Collections.Generic;

namespace Quantum.School.Api.Messages
{
	public class StudentResponse : PersonResponse
	{
		public decimal? Gpa{ get; set; }

		public IEnumerable<ClassScheduleResponse> ClassSchedules { get; set; }
	}
}
