import { Person } from './person';

export interface Student extends Person
{
	gpa?: number;
}
