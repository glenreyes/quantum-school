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
	public class StudentsController : BaseController
	{
		private readonly IStudentRepository studentRepository;

		public StudentsController
		(
			ILogger<BaseController> logger,
			IStudentRepository studentRepository
		) : base
		(
			logger
		)
		{
			this.studentRepository = studentRepository;
		}

		// GET /students
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<IEnumerable<StudentResponse>> GetAll()
		{
			try
			{
				var xxx = studentRepository.GetAll().ToList();

				//TODO: This should be automapped
				var students = studentRepository.GetAll()
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
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<StudentResponse> Get(Guid id)
		{
			try
			{
				var student = studentRepository.Get(id);
				if (student != null)
				{
					//TODO: This should be automapped
					var classes = student.ClassSchedules;
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
		public ActionResult<NewRecordResponse> Post([FromBody] StudentRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var student = new Student
				{
					FirstName = request.FirstName,
					MiddleName = request.MiddleName,
					LastName = request.LastName,
					BirthDate = request.BirthDate,
					Gender = Gender.FromId(request.Gender) ?? Gender.NotSpecified,
					GPA = request.Gpa
				};

				this.studentRepository.Create(student);

				var response = new NewRecordResponse(student.Id);

				return CreatedAtAction(nameof(Get), new { id = student.Id }, response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /students/:id
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Put(Guid id, [FromBody] StudentRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
			var student = studentRepository.Get(id);
				if (student != null)
				{
					student.FirstName = request.FirstName;
					student.MiddleName = request.MiddleName;
					student.LastName = request.LastName;
					student.BirthDate = request.BirthDate;
					student.Gender = Gender.FromId(request.Gender) ?? Gender.NotSpecified;
					student.GPA = request.Gpa;
					this.studentRepository.Update(student);

					return Ok();

					//TODO: Add audit info in response
					//var response = new UpdateStudentResponse(student.Id);
					//return Ok(response);
				}

				return NotFound();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// DELETE /students/:id
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Delete(Guid id)
		{
			try
			{
				var student = studentRepository.Get(id);
				if (student != null)
				{
					this.studentRepository.Delete(student);

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
