import { environment } from '@environment';

import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

//import { SelectListItem } from '@app/models/select-list-item';

export class BaseService<T, TSend>
{
	protected http: HttpClient;
	protected baseAddress: string;

	public options =
	{
		headers: new HttpHeaders(),
		params: new HttpParams()
	};

	constructor
	(
		http: HttpClient,
		baseAddress: string = ''
	)
	{
		this.http = http;
		this.baseAddress = baseAddress;
		this.setHeaders();
	}

	protected setHeaders()
	{
		this.options.headers = new HttpHeaders()
			.set('Content-Type', 'application/json')
			.set('Accept', 'application/json');
	}

	// protected extractSelectListValues(list: SelectListItem[]): string
	// {
	// 	let values: string[] = [];
	// 	list
	// 		.filter(i => i.selected)
	// 		.forEach(i => { values.push(i.value); });
	// 	return values.join(',');
	// }

	protected findEntities(params: HttpParams = null, endpoint: string = ''): Observable<Array<T>>
	{
		endpoint = `${this.baseAddress}${endpoint}`;

		if (params)
			this.options.params = params;

		return this.http
			.get<Array<T>>(endpoint, this.options)
			.pipe(catchError(this.handleError));
	}

	protected getEntity(id: string, params: HttpParams = null, endpoint: string = ''): Observable<T>
	{
		endpoint = `${this.baseAddress}${endpoint}`;

		if (params)
			this.options.params = params;

		return this.http
			.get<T>(`${endpoint}/${id}`, this.options)
			.pipe(catchError(this.handleError));
	}

	protected addEntity(data: TSend, endpoint: string = ''): Observable<T>
	{
		endpoint = `${this.baseAddress}${endpoint}`;

		return this.http
			.post<T>(endpoint, data, this.options)
			.pipe(catchError(this.handleError));
	}

	protected updateEntity(id: string, data: TSend, endpoint: string = ''): Observable<T>
	{
		endpoint = `${this.baseAddress}${endpoint}`;

		return this.http
			.put<T>(`${endpoint}/${id}`, data, this.options)
			.pipe(catchError(this.handleError));
	}

	protected deleteEntity(id: string, endpoint: string = ''): Observable<T>
	{
		endpoint = `${this.baseAddress}${endpoint}`;

		return this.http
			.delete<T>(`${endpoint}/${id}`, this.options)
			.pipe(catchError(this.handleError));

	}

	protected extractData(res: Response)
	{
		let data = res.json() || [];
		return data;
	}

	protected handleError(error: any)
	{
		let errMsg =
			(error.message) ? error.message
				: error.status ? `${error.status} - ${error.statusText}`
					: 'API error';

		console.error(errMsg);

		return throwError(errMsg);
	}
}
