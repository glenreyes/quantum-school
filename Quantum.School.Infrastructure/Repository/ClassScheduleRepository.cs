using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

namespace Quantum.School.Infrastructure.Repository
{
	public class ClassScheduleRepository : EntityRepository<ClassSchedule>, IClassScheduleRepository
	{
		public ClassScheduleRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.Subject)
					.Include(x => x.Teacher)
					.Include(x => x.Students)
					.OrderBy(x => x.Subject.Name)
			);
		}
	}
}
