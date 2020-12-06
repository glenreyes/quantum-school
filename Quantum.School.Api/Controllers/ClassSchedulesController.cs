using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

using Quantum.School.Api.Messages;

namespace Quantum.School.Api
{
	[ApiController]
	[Route("class-schedules")]
	[Produces(MediaTypeNames.Application.Json)]
	public class ClassSchedulesController : BaseController
	{
		//TODO: Move this to a business service
		private readonly IClassScheduleRepository classScheduleRepository;
		private readonly ISubjectRepository subjectRepository;
		private readonly ITeacherRepository teacherRepository;
		private readonly IStudentRepository studentRepository;


		public ClassSchedulesController
		(
			ILogger<BaseController> logger,
			IClassScheduleRepository classScheduleRepository,
			ISubjectRepository subjectRepository,
			ITeacherRepository teacherRepository,
			IStudentRepository studentRepository

		) : base
		(
			logger
		)
		{
			this.classScheduleRepository = classScheduleRepository;
			this.subjectRepository = subjectRepository;
			this.teacherRepository = teacherRepository;
			this.studentRepository = studentRepository;
		}

		// GET /class-schedules
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<IEnumerable<ClassScheduleResponse>> GetAll()
		{
			try
			{
				//TODO: This should be automapped
				var classSchedules = classScheduleRepository.GetAll()
					.Select(classSchedule => new ClassScheduleResponse
					{
						Id = classSchedule.Id,
						Location = classSchedule.Location,

						Subject = new SubjectResponse
						{
							Id = classSchedule.Subject.Id,
							Name = classSchedule.Subject.Name
						},

						Teacher = new TeacherResponse
						{
							Id = classSchedule.Teacher.Id,
							Title = classSchedule.Teacher.Title.Id,
							FirstName = classSchedule.Teacher.FirstName,
							LastName = classSchedule.Teacher.LastName,
						},

						Students = classSchedule.Students.Select(student => new StudentResponse
						{
							Id = student.Id,
							FirstName = student.FirstName,
							LastName = student.LastName
						})
					});

				return Ok(classSchedules);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /class-schedules/:id
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<ClassScheduleResponse> Get(Guid id)
		{
			try
			{
				//if (Guid.TryParse(id, out var guid))
				//{
					var classSchedule = classScheduleRepository.Get(id);
					if (classSchedule != null)
					{
						//TODO: This should be automapped
						var result = new ClassScheduleResponse
						{
							Id = classSchedule.Id,
							Location = classSchedule.Location,

							Subject = new SubjectResponse
							{
								Id = classSchedule.Subject.Id,
								Name = classSchedule.Subject.Name
							},

							Teacher = new TeacherResponse
							{
								Id = classSchedule.Teacher.Id,
								Title = classSchedule.Teacher.Title.Id,
								FirstName = classSchedule.Teacher.FirstName,
								LastName = classSchedule.Teacher.LastName,
							},

							Students = classSchedule.Students.Select(student => new StudentResponse
							{
								Id = student.Id,
								FirstName = student.FirstName,
								LastName = student.LastName
							})
						};
						return Ok(result);
					}
				//}

				return NotFound();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<NewRecordResponse> Post([FromBody] ClassScheduleRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				//List<Student> students = new List<Student>();
				//if (request.Students != null)
				//{
				//	students = request.Students.Select(studentId => new Student
				//	{
				//		Id = studentId
				//	}).ToList();
				//}

				//TODO: This is sloppy, fix this :(

				var subject = subjectRepository.Get(request.Subject);
				if (subject == null)
					return BadRequest();

				var teacher = teacherRepository.Get(request.Teacher);
				if (subject == null)
					return BadRequest();

				var classSchedule = new ClassSchedule
				{
					Location = request.Location,
					Subject = subject,
					Teacher = teacher
				};

				this.classScheduleRepository.Create(classSchedule);

				var response = new NewRecordResponse(classSchedule.Id);

				return CreatedAtAction(nameof(Get), new { id = classSchedule.Id }, response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /classSchedules/:id
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Put(Guid id, [FromBody] ClassScheduleRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var classSchedule = classScheduleRepository.Get(id);
				if (classSchedule != null)
				{
					classSchedule.Location = request.Location;

					//TODO: Again, this is sloppy, fix this :(

					var subject = subjectRepository.Get(request.Subject);
					if (subject == null)
						return BadRequest();

					var teacher = teacherRepository.Get(request.Teacher);
					if (subject == null)
						return BadRequest();

					classSchedule.Subject = subject;
					classSchedule.Teacher = teacher;

					this.classScheduleRepository.Update(classSchedule);

					return Ok();

					//TODO: Add audit info in response
					//var response = new UpdateClassScheduleResponse(classSchedule.Id);
					//return Ok(response);
				}

				return NotFound();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// DELETE /classSchedules/:id
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Delete(Guid id)
		{
			try
			{
				var classSchedule = classScheduleRepository.Get(id);
				if (classSchedule != null)
				{
					this.classScheduleRepository.Delete(classSchedule);

					return Ok();
				}

				return NotFound();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}
