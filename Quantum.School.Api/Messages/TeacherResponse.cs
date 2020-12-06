using System;
using System.Collections.Generic;

namespace Quantum.School.Api.Messages
{
	public class TeacherResponse : PersonResponse
	{
	 	public string Title { get; set; }

		public IEnumerable<ClassScheduleResponse> ClassSchedules { get; set; }
	}
}
