using System;
using System.Linq;

using Quantum.School.Core.Models;

namespace Quantum.School.Core.Repository
{
	public interface IEntityRepository<T> where T : BaseEntity
	{
		void Create(T entity);

		void Update(T entity);

		void Delete(T entity);

		void Delete(object id);

		/// <summary>
		///		Get the entity that matches the provided Id
		/// </summary>
		/// <returns>Single entity</returns>
		T Get(object id);
		T Find(object id);

		/// <summary>
		///		Get all entities
		/// </summary>
		IQueryable<T> GetAll();
	}
}
