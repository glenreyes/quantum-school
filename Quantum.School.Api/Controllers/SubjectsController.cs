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
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class SubjectsController : BaseController
	{
		private readonly ISubjectRepository subjectRepository;

		public SubjectsController
		(
			ILogger<BaseController> logger,
			ISubjectRepository subjectRepository
		) : base
		(
			logger
		)
		{
			this.subjectRepository = subjectRepository;
		}

		// GET /subjects
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<IEnumerable<SubjectResponse>> GetAll()
		{
			try
			{
				//TODO: This should be automapped
				var subjects = subjectRepository.GetAll()
					.Select(subject => new SubjectResponse
					{
						Id = subject.Id,
						Name = subject.Name,
						Description = subject.Description,
						Teachers = subject.Teachers.Select(teacher => new TeacherResponse
						{
							Id = teacher.Id,
							Title = teacher.Title.Id,
							FirstName = teacher.FirstName,
							LastName = teacher.LastName
						})
					});

				return Ok(subjects);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /subjects/:id
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult<SubjectResponse> Get(Guid id)
		{
			try
			{
				var subject = subjectRepository.Get(id);
				if (subject != null)
				{
					//TODO: This should be automapped
					var result = new SubjectResponse
					{
						Id = subject.Id,
						Name = subject.Name,
						Description = subject.Description,
						Teachers = subject.Teachers.Select(teacher => new TeacherResponse
						{
							Id = teacher.Id,
							Title = teacher.Title.Id,
							FirstName = teacher.FirstName,
							LastName = teacher.LastName
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
		public ActionResult<NewRecordResponse> Post([FromBody] SubjectRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var subject = new Subject
				{
					Name = request.Name,
					Description = request.Description
				};

				this.subjectRepository.Create(subject);

				var response = new NewRecordResponse(subject.Id);

				return CreatedAtAction(nameof(Get), new { id = subject.Id }, response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /subjects/:id
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Put(Guid id, [FromBody] SubjectRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var subject = subjectRepository.Get(id);
				if (subject != null)
				{
					subject.Name = request.Name;
					subject.Description = request.Description;

					this.subjectRepository.Update(subject);

					return Ok();

					//TODO: Add audit info in response
					//var response = new UpdateSubjectResponse(subject.Id);
					//return Ok(response);
				}

				return NotFound();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// DELETE /subjects/:id
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Delete(Guid id)
		{
			try
			{
				var subject = subjectRepository.Get(id);
				if (subject != null)
				{
					this.subjectRepository.Delete(subject);

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
