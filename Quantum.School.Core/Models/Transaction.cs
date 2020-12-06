using System;
using System.Threading.Tasks;

namespace Quantum.School.Core.Models
{
	public class Transaction
	{
		public DateTimeOffset TransactionDate { get; set; }

		public virtual User TransactionBy { get; set; }
		public Guid? TransactionById { get; set; }

		public string TransactionAgent { get; set; }

		public string TransactionRemoteAddress { get; set; }

		public string TransactionDetails { get; set; }

		public string TransactionUserNotes { get; set; }
	}
}
