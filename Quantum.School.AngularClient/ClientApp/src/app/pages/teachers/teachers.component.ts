import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { TeacherManagementService } from '@app/services';
import { Teacher, Gender, NameTitle } from '@app/models';
import { BasePage } from '../base-page.component';

import * as referenceTable from '@app/models/person';

@Component
({
	selector: 'app-teachers',
	templateUrl: './teachers.component.html',
	styleUrls: ['./teachers.component.css']
})
export class TeachersComponent extends BasePage<Teacher> implements OnInit
{
	public genderList: Gender[] = referenceTable.GenderList;
	public nameTitleList: NameTitle[] = referenceTable.NameTitleList;

	constructor
	(
		private pageDataManagementService: TeacherManagementService,
		private formBuilder: FormBuilder,
	)
	{
		super();
	}

	ngOnInit()
	{
		this.createForm();
		this.loadData();
	}

	createForm()
	{
		this.form = this.formBuilder.group
		({
			id: [],
			title: [],
			firstName: ['', Validators.required],
			middleName: [],
			lastName: ['', Validators.required],
			gender: [],
			birthDate: ['', Validators.required]
		});
	}

	loadData()
	{
		this.isLoading = true;
		this.pageDataManagementService
			.getAll()
			.subscribe
			(
				data  => { this.list = data; },
				error => { console.log('Error at teachers.component.ts', error); },
				() => { this.isLoading = false; }
			);
	}

	add()
	{
		this.selectedItem =
		{
			id: ''
		};
		this.setFormValues(this.selectedItem);
	}

	edit(item: Teacher)
	{
		this.selectedItem = item;
		this.setFormValues(this.selectedItem);
	}

	delete(item: Teacher)
	{
		// Sorry, this is quicker to setup than modal :D
		let result = confirm(`Are you sure you want to delete "${item.title} ${item.firstName} ${item.lastName}"?`);
		if (result)
		{
			this.pageDataManagementService
				.delete(item)
				.subscribe
				(
					data => { this.removeFromList(item); },
					error => { console.log('Error at teachers.component.ts', error); },
					() => { }
				);
		}
	}

	setFormValues(item: Teacher)
	{
		this.form.controls['id'].setValue(item.id);
		this.form.controls['title'].setValue(item.title);
		this.form.controls['firstName'].setValue(item.firstName);
		this.form.controls['middleName'].setValue(item.middleName);
		this.form.controls['lastName'].setValue(item.lastName);
		this.form.controls['gender'].setValue(item.gender);
		this.form.controls['birthDate'].setValue(this.dateToPicker(item.birthDate));
	}

	formCancel()
	{
		this.selectedItem = undefined;
		this.form.reset();
	}

	formSubmit()
	{
		this.selectedItem = this.form.value as Teacher;

		this.selectedItem.birthDate = this.pickerToDate(this.form.controls['birthDate'].value);

		if (this.selectedItem.id)
		{
			this.pageDataManagementService
				.update(this.selectedItem)
				.subscribe
				(
					data => { this.removeFromList(this.selectedItem); },
					error => { console.log('Error at teachers.component.ts', error); },
					() => { this.formCancel(); this.loadData(); }
				);
		}
		else
		{
			this.pageDataManagementService
				.add(this.selectedItem)
				.subscribe
				(
					data => { this.removeFromList(this.selectedItem); },
					error => { console.log('Error at teachers.component.ts', error); },
					() => { this.formCancel(); this.loadData(); }
				);
		}
	}

}
