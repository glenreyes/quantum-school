using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

namespace Quantum.School.Infrastructure.Repository
{
	public class TeacherRepository : EntityRepository<Teacher>, ITeacherRepository
	{
		public TeacherRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.ClassSchedules)
			);
		}
	}
}
