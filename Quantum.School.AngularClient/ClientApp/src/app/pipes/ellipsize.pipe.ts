import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

/*
 * Truncates the text when it exceeds the maxlength value and appends an ellipsis.
 * Usage:
 *   value | ellipsize(maxlength)
 * Example:
 *   {{ "Hello I've waited here for you" | ellipsize(10) }}
 *   formats to: "Hello I've..."
*/
@Pipe({ name: 'ellipsize' })
export class EllipsizePipe implements PipeTransform
{
	constructor(private sanitized: DomSanitizer) { }

	transform(value: string, maxlength: number): any
	{
		let result: any = '';

		if (value)
		{
			result = (value.length > maxlength)
				? `${value.substring(0, maxlength - 1)}&hellip;`
				: value;
		}

		return this.sanitized.bypassSecurityTrustHtml(result);
	}
}
