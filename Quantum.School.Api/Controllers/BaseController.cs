using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Quantum.School.Api
{
	public class BaseController : ControllerBase
	{
		protected readonly ILogger<BaseController> logger;

		public BaseController
		(
			ILogger<BaseController> logger
		)
		{
			this.logger = logger;
		}

		protected ObjectResult GenericServerErrorResult
		(
			Exception exception = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerFilePath] string callerFilePath = null,
			[CallerMemberName] string callerMemberName = null
		)
		{
			return ErrorResult
			(
				exception: exception,
				status: StatusCodes.Status500InternalServerError,
				title: $"Quantum School API failed",
				detail: $"Something went wrong with the API. Please notify Quantum support with the error. Please attach this number for reference: #LOG-ID#",
				lineNumber: lineNumber,
				callerFilePath: callerFilePath,
				callerMemberName: callerMemberName
			);
		}

		protected ObjectResult ErrorResult
		(
			int? status,
			string title,
			string detail,
			Exception exception = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerFilePath] string callerFilePath = null,
			[CallerMemberName] string callerMemberName = null
		)
		{
			var logId = Guid.NewGuid().ToString();

			title = title.Replace("#LOG-ID#", logId);
			detail = detail.Replace("#LOG-ID#", logId);

			var log =
				$"LogId: {logId} \n" +
				$"Title: {title} \n" +
				$"Detail: {detail} \n" +
				$"Source: {callerFilePath} {callerMemberName} at line {lineNumber} \n";

			if (exception != null)
				log +=
					$"ExceptionSource: {exception.Source} \n" +
					$"ExceptionMessage: {exception.Message} \n" +
					$"ExceptionInner: {exception.InnerException} \n" +
					$"ExceptionStackTrace: {exception.StackTrace} \n";

			logger.LogError(log);

			var problemDetails = new ProblemDetails
			{
				Status = status,
				Type = "about:blank",
				Title = title,
				Detail = detail,
				Instance = HttpContext.Request.Path
			};

			return new ObjectResult(problemDetails)
			{
				ContentTypes = { "application/problem+json" },
				StatusCode = status,
			};
		}
	}
}
