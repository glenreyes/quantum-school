import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

export class BasePage<T>
{
	public selectedItem: T;
	public list: T[] = [];
	public isLoading: boolean = true;
	public form: FormGroup;

	constructor() { }

	public formCancel()
	{
		this.selectedItem = undefined;
		this.form.reset();
	}

	public removeFromList(itemToRemove: T)
	{
		this.list.forEach( (item, index) =>
		{
			if (item === itemToRemove)
				this.list.splice(index, 1);
		});
	}

	public dateToPicker(value: any): NgbDateStruct
	{
		let date = new Date(Date.parse(value));
		return value ? {
				year: date.getUTCFullYear(),
				month: date.getUTCMonth() + 1,
				day: date.getUTCDate()
			} : null;
	}

	public pickerToDate(value: NgbDateStruct): Date
	{
		return new Date(Date.UTC(value.year, value.month - 1, value.day, 0, 0, 0));
	}
}
