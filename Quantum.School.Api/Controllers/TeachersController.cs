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
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class TeachersController : BaseController
	{
		private readonly ITeacherRepository teacherRepository;
		private readonly IClassScheduleRepository classScheduleRepository;

		public TeachersController
		(
			ILogger<BaseController> logger,
			ITeacherRepository teacherRepository,
			IClassScheduleRepository classScheduleRepository
		) : base
		(
			logger
		)
		{
			this.teacherRepository = teacherRepository;
			this.classScheduleRepository = classScheduleRepository;
		}

		// GET /teachers
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<IEnumerable<TeacherResponse>> GetAll()
		{
			try
			{
				//TODO: This should be automapped
				var teachers = teacherRepository.GetAll()
					.Select(teacher => new TeacherResponse
					{
						Id = teacher.Id,
						FirstName = teacher.FirstName,
						MiddleName = teacher.MiddleName,
						LastName = teacher.LastName,
						BirthDate = teacher.BirthDate,
						Gender = teacher.Gender.Id,
						Title = teacher.Title.Id,
						ClassSchedules = teacher.ClassSchedules.Select(cs => new ClassScheduleResponse
						{
							Id = cs.Id,
							Location = cs.Location
						})
					});

				return Ok(teachers);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /teachers/:id
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<TeacherResponse> Get(Guid id)
		{
			try
			{
				var teacher = teacherRepository.Get(id);
				if (teacher != null)
				{
					var classSchedules = classScheduleRepository
						.GetAll()
						.Where(cs => cs.Teacher.Id == id);

					//TODO: This should be automapped
					var result = new TeacherResponse
					{
						Id = teacher.Id,
						FirstName = teacher.FirstName,
						MiddleName = teacher.MiddleName,
						LastName = teacher.LastName,
						BirthDate = teacher.BirthDate,
						Gender = teacher.Gender.Id,
						Title = teacher.Title.Id,
						ClassSchedules = teacher.ClassSchedules.Select(cs => new ClassScheduleResponse
						{
							Id = cs.Id,
							Location = cs.Location
						})
					};
					return Ok(result);
				}

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
		public ActionResult<NewRecordResponse> Post([FromBody] TeacherRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var teacher = new Teacher
				{
					FirstName = request.FirstName,
					MiddleName = request.MiddleName,
					LastName = request.LastName,
					BirthDate = request.BirthDate,
					Gender = Gender.FromId(request.Gender) ?? Gender.NotSpecified,
					Title = NameTitle.FromId(request.Title) ?? NameTitle.None
				};

				this.teacherRepository.Create(teacher);

				var response = new NewRecordResponse(teacher.Id);

				return CreatedAtAction(nameof(Get), new { id = teacher.Id }, response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /teachers/:id
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Put(Guid id, [FromBody] TeacherRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var teacher = teacherRepository.Get(id);
				if (teacher != null)
				{
					teacher.FirstName = request.FirstName;
					teacher.MiddleName = request.MiddleName;
					teacher.LastName = request.LastName;
					teacher.BirthDate = request.BirthDate;
					teacher.Gender = Gender.FromId(request.Gender) ?? Gender.NotSpecified;
					teacher.Title = NameTitle.FromId(request.Title) ?? NameTitle.None;

					this.teacherRepository.Update(teacher);

					return Ok();

					//TODO: Add audit info in response
					//var response = new UpdateTeacherResponse(teacher.Id);
					//return Ok(response);
				}

				return NotFound();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// DELETE /teachers/:id
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Delete(Guid id)
		{
			try
			{
				var teacher = teacherRepository.Get(id);
				if (teacher != null)
				{
					this.teacherRepository.Delete(teacher);

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
