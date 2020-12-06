using System;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class PersonRequest
	{
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[StringLength(50)]
		public string MiddleName { get; set; }

		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required]
		public DateTime? BirthDate { get; set; }

		[StringLength(6)]
		public string Gender { get; set; }
	}
}
