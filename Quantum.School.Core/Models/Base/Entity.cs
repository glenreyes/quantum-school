using System;

namespace Quantum.School.Core.Models
{
	public class BaseEntity
	{
		public virtual object Id { get; set; }
	}

	public abstract class Entity<TKey> : BaseEntity, IEntity<TKey>
	{
		public virtual new TKey Id { get; set; }
	}
}
