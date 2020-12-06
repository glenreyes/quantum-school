using System;

namespace Quantum.School.Core.Models
{
	public class User : Entity<Guid>
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }
	}
}
