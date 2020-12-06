using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.School.Core.Models
{
	public interface IAuditableEntity
	{
		DateTimeOffset TransactionDate { get; set; }

		User TransactionBy { get; set; }

		string TransactionAgent { get; set; }

		string TransactionRemoteAddress { get; set; }

		string TransactionDetails { get; set; }
	}
}
