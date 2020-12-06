// NOTE:

// Checking whether GLEN is enrolled in SCIENCE (200 vs. 404)
//GET /class-schedules/SCIENCE/students/GLEN

// Checking whether GLEN is also enrolled in MATH
//GET /class-schedules/MATH/students/GLEN

// Adding GLEN also to HISTORY class
//PUT /class-schedules/HISTORY/students/GLEN

// Getting all students of SCIENCE class
//GET /class-schedules/SCIENCE/students

// Getting all class schedules of GLEN
//GET /students/GLEN/class-schedules

// Kickout GLEN out of SCIENCE class
//DELETE /class-schedules/SCIENCE/students/GLEN

// Register a new student to the school, return ID (LANIE)
//POST /students

//# Now add LANIE to SCIENCE class
//PUT /class-schedules/SCIENCE/students/LANIE



using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Quantum.School.Core.Models;
using Quantum.School.Core.Business;
using Quantum.School.Core.Repository;

using Quantum.School.Api.Messages;

namespace Quantum.School.Api
{
	[ApiController]
	[Route("class-schedules/{classScheduleId}/students")]
	[Produces(MediaTypeNames.Application.Json)]
	public class ClassScheduleStudentsController : BaseController
	{
		private readonly IStudentRepository studentRepository;
		private readonly IClassScheduleRepository classScheduleRepository;

		public ClassScheduleStudentsController
		(
			ILogger<BaseController> logger,
			IStudentRepository studentRepository,
			IClassScheduleRepository classScheduleRepository
		) : base
		(
			logger
		)
		{
			this.studentRepository = studentRepository;
			this.classScheduleRepository = classScheduleRepository;
		}

		// GET /students
		// Gets all students under the specified class schedule
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<IEnumerable<StudentResponse>> GetAll(Guid classScheduleId)
		{
			try
			{
				var classSchedule = classScheduleRepository.Get(classScheduleId);
				if (classSchedule == null)
					return NotFound();

				// TODO: This should be automapped
				var students = classSchedule.Students
					.Select(student => new StudentResponse
					{
						Id = student.Id,
						FirstName = student.FirstName,
						MiddleName = student.MiddleName,
						LastName = student.LastName,
						BirthDate = student.BirthDate,
						Gender = student.Gender.Id,
						Gpa = student.GPA
					});

				return Ok(students);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /students/:id
		[HttpGet("{studentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<StudentResponse> Get(Guid classScheduleId, Guid studentId)
		{
			try
			{
				var classSchedule = classScheduleRepository.Get(classScheduleId);
				if (classSchedule == null)
					return NotFound();

				var student = classSchedule.Students.FirstOrDefault(x => x.Id == studentId);
				if (student == null)
					return NotFound();

				var result = new StudentResponse
				{
					Id = student.Id,
					FirstName = student.FirstName,
					MiddleName = student.MiddleName,
					LastName = student.LastName,
					BirthDate = student.BirthDate,
					Gender = student.Gender.Id,
					Gpa = student.GPA
				};

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /students/:id
		[HttpPut("{studentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Put(Guid classScheduleId, Guid studentId)
		{
			try
			{
				var classSchedule = classScheduleRepository.Get(classScheduleId);
				if (classSchedule == null)
					return NotFound();

				// Cannot assign to class non existing students
				var student = studentRepository.Get(studentId);
				if (student == null)
					return NotFound();

				//student.ClassSchedules.Add(classSchedule);
				//this.studentRepository.Update(student);

				classSchedule.Students.Add(student);
				classScheduleRepository.Update(classSchedule);

				return Ok();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// DELETE /students/:id
		[HttpDelete("{studentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Delete(Guid classScheduleId, Guid studentId)
		{
			try
			{
				var classSchedule = classScheduleRepository.Get(classScheduleId);
				if (classSchedule == null)
					return NotFound();

				// Cannot assign to class non existing students
				var student = studentRepository.Get(studentId);
				if (student == null)
					return NotFound();

				classSchedule.Students.Remove(student);
				classScheduleRepository.Update(classSchedule);

				return Ok();

			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}
