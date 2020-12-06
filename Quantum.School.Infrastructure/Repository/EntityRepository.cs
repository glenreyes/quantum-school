using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

namespace Quantum.School.Infrastructure.Repository
{
	public abstract class EntityRepository<T> : IEntityRepository<T>
		where T : BaseEntity
	{
		protected IApplicationContext context;
		protected DbSet<T> dbset;

		protected IQueryable<T> dataset;

		public EntityRepository(IApplicationContext context)
		{
			this.context = context
				?? throw new ArgumentNullException(nameof(context), "Context cannot be null");

			this.dbset = this.context.Set<T>();
		}

		public virtual T Get(object id)
		{
			//This is more efficient
			//return this.dbset.Find(id);

			//But I can use include here XD
			//return this.dbset.FirstOrDefault(x => x.Id == id);
			return this.dataset.FirstOrDefault(x => x.Id == id);
		}

		public virtual T Find(object id)
		{
			return this.dbset.Find(id);
		}

		public virtual void Create(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity), "Cannot create null entity");

			this.dbset.Add(entity);
			this.context.SaveChanges();
		}


		public virtual void Update(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity), "Cannot update null entity");

			this.dbset.Update(entity);
			this.context.SaveChanges();
		}

		public virtual void Delete(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity), "Cannot delete null entity");

			this.dbset.Remove(entity);
			this.context.SaveChanges();
		}

		public virtual void Delete(object id)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id), "Parameter id cannot be null");

			var entity = new BaseEntity
			{
				Id = id
			};

			this.dbset.Remove((T)entity);
			this.context.SaveChanges();
		}

		public virtual IQueryable<T> GetAll()
		{
			return this.dataset;
		}

		protected virtual void InitializeDataSet(IQueryable<T> dataset)
		{
			this.dataset = dataset;
		}
	}
}
