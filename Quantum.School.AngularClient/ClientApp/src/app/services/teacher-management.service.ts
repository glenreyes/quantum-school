import { environment } from '@environment';

import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Teacher } from '@app/models';
import { BaseService } from './base.service';

@Injectable()
export class TeacherManagementService extends BaseService<Teacher, Teacher>
{
	constructor
	(
		http: HttpClient
	)
	{
		super
		(
			http,
			environment.appApi.host + environment.appApi.teachersEndpoint
		);
	}

	public getAll(): Observable<Teacher[]>
	{
		return this.findEntities();
	}

	public get(id: string): Observable<Teacher>
	{
		return this.getEntity(id);
	}

	public add(record: Teacher): Observable<Teacher>
	{
		return this.addEntity(record);
	}

	public update(record: Teacher): Observable<Teacher>
	{
		return this.updateEntity(record.id, record);
	}

	public delete(record: Teacher): Observable<Teacher>
	{
		return this.deleteEntity(record.id);
	}
}
