import { environment } from '@environment';

import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Subject } from '@app/models';
import { BaseService } from './base.service';

@Injectable()
export class SubjectManagementService extends BaseService<Subject, Subject>
{
	constructor
	(
		http: HttpClient
	)
	{
		super
		(
			http,
			environment.appApi.host + environment.appApi.subjectsEndpoint
		);
	}

	public getAll(): Observable<Subject[]>
	{
		return this.findEntities();
	}

	public get(id: string): Observable<Subject>
	{
		return this.getEntity(id);
	}

	public add(record: Subject): Observable<Subject>
	{
		return this.addEntity(record);
	}

	public update(record: Subject): Observable<Subject>
	{
		return this.updateEntity(record.id, record);
	}

	public delete(record: Subject): Observable<Subject>
	{
		return this.deleteEntity(record.id);
	}
}
