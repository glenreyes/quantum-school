using System;
using System.Collections.Generic;

namespace Quantum.School.Core.Models
{
	public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
	{
		public DateTimeOffset TransactionDate { get; set; }

		public virtual User TransactionBy { get; set; }
		public string TransactionById { get; set; }

		public string TransactionAgent { get; set; }

		public string TransactionRemoteAddress { get; set; }

		public string TransactionDetails { get; set; }

		public string TransactionUserNotes { get; set; }
	}
}
