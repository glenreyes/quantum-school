using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class ClassScheduleRequest
	{
		[Required]
		[StringLength(80)]
		public string Location { get; set; }

		[Required]
		public Guid Subject { get; set; }

		[Required]
		public Guid Teacher { get; set; }

		public IEnumerable<Guid> Students { get; set; }
	}
}
