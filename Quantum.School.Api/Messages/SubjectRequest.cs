using System;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class SubjectRequest
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(500)]
		public string Description { get; set; }
	}
}
