using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

namespace Quantum.School.Infrastructure.Repository
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
			);
		}
	}
}
