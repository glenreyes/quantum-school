using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

namespace Quantum.School.Infrastructure.Repository
{
	public class SubjectRepository : EntityRepository<Subject>, ISubjectRepository
	{
		public SubjectRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.Teachers)
					.OrderBy(x => x.Name)
			);
		}
	}
}
