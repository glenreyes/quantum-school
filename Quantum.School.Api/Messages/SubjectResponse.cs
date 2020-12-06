using System;
using System.Collections.Generic;

namespace Quantum.School.Api.Messages
{
	public class SubjectResponse
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public IEnumerable<TeacherResponse> Teachers { get; set; }
	}
}
