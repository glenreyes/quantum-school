import { environment } from '@environment';

import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Student } from '@app/models';
import { BaseService } from './base.service';

@Injectable()
export class StudentManagementService extends BaseService<Student, Student>
{
	constructor
	(
		http: HttpClient
	)
	{
		super
		(
			http,
			environment.appApi.host + environment.appApi.studentsEndpoint
		);
	}

	public getAll(): Observable<Student[]>
	{
		return this.findEntities();
	}

	public get(id: string): Observable<Student>
	{
		return this.getEntity(id);
	}

	public add(record: Student): Observable<Student>
	{
		return this.addEntity(record);
	}

	public update(record: Student): Observable<Student>
	{
		return this.updateEntity(record.id, record);
	}

	public delete(record: Student): Observable<Student>
	{
		return this.deleteEntity(record.id);
	}
}
