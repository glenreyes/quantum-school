using System;
using System.Linq;
using System.Threading;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using Quantum.School.Core.Models;
using System.Collections.Generic;

namespace Quantum.School.Infrastructure.Repository
{
	public interface IApplicationContext : IContext
	{
		// Identity
		DbSet<User> Users { get; set; }

		// Business
		DbSet<ClassSchedule> ClassSchedules { get; set; }
		DbSet<Subject> Subjects { get; set; }
		DbSet<Person> Persons { get; set; }
		DbSet<Teacher> Teachers { get; set; }
		DbSet<Student> Students { get; set; }
	}

	public class ApplicationContext : DbContext, IApplicationContext
	{
		public ApplicationContext()
		{
		}

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
		   : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=localhost;Database=Quantum.School;Trusted_Connection=True;MultipleActiveResultSets=true");
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// I prefer DB First :D

			#region Identity

			builder.Entity<User>(entity =>
			{
				// Table and PK mapping
				entity
					.ToTable("Users")
					.HasKey(e => new { e.Id });
			});

			#endregion

			#region Business

			builder.SharedTypeEntity<Dictionary<string, object>>("Students_ClassSchedules", entity =>
			{
				// Shadow Junction
				entity.Property<Guid>("StudentId");
				entity.Property<Guid>("ClassScheduleId");
			});

			builder.SharedTypeEntity<Dictionary<string, object>>("Subjects_Teachers", entity =>
			{
				// Shadow Junction
				entity.Property<Guid>("SubjectId");
				entity.Property<Guid>("TeacherId");
			});

			builder.Entity<ClassSchedule>(entity =>
			{
				// Table and PK mapping
				entity
					.ToTable("ClassSchedules")
					.HasKey(e => new { e.Id });

				// Shadow FK - SubjectId
				entity.Property<Guid>("SubjectId");
				// ClassSchedules >> Subjects
				entity
					.HasOne(classSchedule => classSchedule.Subject)
					.WithMany(subject => subject.ClassSchedules)
					.HasForeignKey("SubjectId");

				// Shadow FK - TeacherId
				entity.Property<Guid>("TeacherId");
				// ClassSchedules >> Teacher
				entity
					.HasOne(classSchedule => classSchedule.Teacher)
					.WithMany(teacher => teacher.ClassSchedules)
					.HasForeignKey("TeacherId");

				//entity
				//	.HasMany(cs => cs.Students)
				//	.WithMany(s => s.ClassSchedules)
				//	.UsingEntity<Dictionary<string, object>>("Students_ClassSchedules",
				//		junction => junction.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
				//		junction => junction.HasOne<ClassSchedule>().WithMany().HasForeignKey("ClassScheduleId"),
				//		junction => junction.ToTable("Students_ClassSchedules", "Students"));
			});

			builder.Entity<Subject>(entity =>
			{
				// Table and PK mapping
				entity
					.ToTable("Subjects")
					.HasKey(e => new { e.Id });

				// Subjects >< Teachers
				entity
					.HasMany(subject => subject.Teachers)
					.WithMany(teacher => teacher.Subjects)
					.UsingEntity<Dictionary<string, object>>("Subjects_Teachers",
						junction => junction.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId"),
						junction => junction.HasOne<Subject>().WithMany().HasForeignKey("SubjectId"));
			});

			builder.Entity<Person>(entity =>
			{
				// Table and PK mapping
				entity
					.ToTable("Persons")
					.HasKey(e => new { e.Id });

				// Person - Gender - Enumeration
				entity.Ignore(e => e.Gender);
				entity.Property(e => e.GenderId).HasColumnName("Gender");
			});

			builder.Entity<Teacher>(entity =>
			{
				// Table mapping
				entity.ToTable("Teachers");

				// Key is not needed in derived types
				//entity.HasKey(e => new { e.Id });

				// Teacher - NameTitle - Enumeration
				entity.Ignore(e => e.Title);
				entity.Property(e => e.TitleId).HasColumnName("Title");
			});

			builder.Entity<Student>(entity =>
			{
				// Table mapping
				entity.ToTable("Students");

				// Key is not needed in derived types
				//entity.HasKey(e => new { e.Id });

				// Students >< ClassSchedules
				entity
					.HasMany(student => student.ClassSchedules)
					.WithMany(classSchedule => classSchedule.Students)
					.UsingEntity<Dictionary<string, object>>("Students_ClassSchedules",
						junction => junction.HasOne<ClassSchedule>().WithMany().HasForeignKey("ClassScheduleId"),
						junction => junction.HasOne<Student>().WithMany().HasForeignKey("StudentId"));
			});

			#endregion
		}

		public override int SaveChanges()
		{
			//TODO:
			//Figure out an elegant approach
			/*
			var modifiedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is IAuditableEntity
					&& (x.State == EntityState.Added || x.State == EntityState.Modified));

			//var currentUser = ClaimsPrincipal.Current;

			foreach (var entry in modifiedEntries)
			{
				IAuditableEntity entity = entry.Entity as IAuditableEntity;
				if (entity != null)
				{
					//string identityName = Thread.CurrentPrincipal.Identity.Name;

					//TODO:
					//Figure out an elegant approach
					//string identityName = currentPrincipalAccessor.CurrentPrincipal.FindFirstValue(ClaimTypes.GivenName);
					//string userAgent = currentApplicationUser.CurrentRequest.Headers["User-Agent"].ToString();
					//string remoteAddress = currentPrincipalAccessor.RemoteAdress.ToString();
					//DateTime timestamp = DateTime.UtcNow;

					if (entry.State == EntityState.Deleted)
					{
						//TODO:
						//Change to soft delete for audit trail then let a DB trigger
						//handle the actual delete after inserting to the audit table

						//entity.CreatedBy = identityName;
						//entity.CreatedDate = timestamp;
					}
					else
					{
						//base.Entry(entity).Property(x => x.TransactionBy).IsModified = true;
						//base.Entry(entity).Property(x => x.TransactionDate).IsModified = true;
					}

					//entity.TransactionBy = "";
					//entity.TransactionDate = DateTime.UtcNow;
					//entity.TransactionAgent = "";
					//entity.TransactionRemoteAddress = "";
				}
			}
			*/
			return base.SaveChanges();
		}

		// Identity
		public DbSet<User> Users { get; set; }

		// Business
		public DbSet<ClassSchedule> ClassSchedules { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Student> Students { get; set; }
	}
}
