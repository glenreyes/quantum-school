import { environment } from '@environment';

import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ClassSchedule, ClassScheduleEntry, Student } from '@app/models';
import { BaseService } from './base.service';

@Injectable()
export class ClassScheduleManagementService extends BaseService<ClassSchedule, ClassScheduleEntry>
{
	constructor
	(
		http: HttpClient
	)
	{
		super
		(
			http,
			environment.appApi.host + environment.appApi.classSchedulesEndpoint
		);
	}

	public getAll(): Observable<ClassSchedule[]>
	{
		return this.findEntities();
	}

	public get(id: string): Observable<ClassSchedule>
	{
		return this.getEntity(id);
	}

	public add(record: ClassScheduleEntry): Observable<ClassSchedule>
	{
		return this.addEntity(record);
	}

	public update(id: string, record: ClassScheduleEntry): Observable<ClassSchedule>
	{
		return this.updateEntity(id, record);
	}

	public delete(id: string): Observable<ClassSchedule>
	{
		return this.deleteEntity(id);
	}


	// Get all students under the specified class schedule
	public getStudents(classScheduleId: string): Observable<Array<Student>>
	{
		let endpoint = `${this.baseAddress}/${classScheduleId}/students`;

		return this.http
			.get<Array<Student>>(endpoint, this.options)
			.pipe(catchError(this.handleError));
	}

	// Add existing student to a class schedule
	public addStudent(classScheduleId: string, studentId: string): Observable<Student>
	{
		let endpoint = `${this.baseAddress}/${classScheduleId}/students/${studentId}`;
		let data = undefined;

		return this.http
			.put<Student>(endpoint, data, this.options)
			.pipe(catchError(this.handleError));
	}

	// Remove student from class schedule
	public removeStudent(classScheduleId: string, studentId: string): Observable<Student>
	{
		let endpoint = `${this.baseAddress}/${classScheduleId}/students/${studentId}`;

		return this.http
			.delete<Student>(endpoint, this.options)
			.pipe(catchError(this.handleError));
	}
}
