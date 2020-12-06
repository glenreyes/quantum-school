using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class StudentRequest : PersonRequest
	{
		[Required]
		public decimal Gpa { get; set; }

		public IEnumerable<Guid> ClassSchedules { get; set; }
	}
}
