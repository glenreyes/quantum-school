using System;
using System.ComponentModel.DataAnnotations;

namespace Quantum.School.Api.Messages
{
	public class NewRecordResponse
	{
		public NewRecordResponse()
		{
		}

		public NewRecordResponse(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; init; }
	}
}
