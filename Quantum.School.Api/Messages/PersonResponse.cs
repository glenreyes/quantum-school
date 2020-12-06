using System;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class PersonResponse
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public DateTime? BirthDate { get; set; }

		public int? Age
		{
			get
			{
				if (BirthDate.HasValue)
				{
					TimeSpan span = DateTime.Now - BirthDate.Value;
					DateTime age = DateTime.MinValue + span;

					int years = age.Year - 1;

					return years;
				}

				return null;
			}
		}

		public string Gender { get; set; }
	}
}
