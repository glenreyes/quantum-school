using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class TeacherRequest : PersonRequest
	{
		public string Title { get; set; }

		public IEnumerable<Guid> Subjects { get; set; }
	}
}
