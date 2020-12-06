using System;
using System.Collections.Generic;

namespace Quantum.School.Core.Models
{
	//TODO: Implement Student ID instead of generated GUID
	public class Student : Person
	{
		// There's a bug when using decimal properties
		// https://github.com/OmniSharp/omnisharp-vscode/issues/3926
		// https://github.com/dotnet/efcore/issues/23447
		public decimal? GPA { get; set; }

		public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
	}
}
