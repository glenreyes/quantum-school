using System;
using Quantum.School.Core.Models;

namespace Quantum.School.Core.Business
{
	public interface IEnrollmentManagement
	{
		ClassSchedule CreateClassSchedule(Guid subjectId, Guid teacherId, string location);

		void EnrollStudent(Guid classScheduleId, Guid studentId);
	}
}
