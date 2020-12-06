using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

namespace Quantum.School.Infrastructure.Repository
{
	public class PersonRepository : EntityRepository<Person>, IPersonRepository
	{
		public PersonRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.OrderBy(x => x.LastName)
					.ThenBy(x => x.FirstName)
			);
		}
	}
}
