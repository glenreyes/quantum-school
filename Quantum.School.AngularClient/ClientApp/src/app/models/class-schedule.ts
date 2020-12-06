import { BaseModel } from './base-model';
import { Subject } from './subject';
import { Teacher } from './teacher';
import { Student } from './student';

export interface ClassSchedule extends BaseModel
{
	location: string;

	subject: Subject;

	teacher: Teacher;

	students: Student[];
}

export interface ClassScheduleEntry
{
	location: string;

	subject: string;

	teacher: string;

	students: Student[];
}
