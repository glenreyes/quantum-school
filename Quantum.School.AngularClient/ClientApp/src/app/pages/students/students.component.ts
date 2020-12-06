import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { StudentManagementService } from '@app/services';
import { Student, Gender } from '@app/models';
import { BasePage } from '../base-page.component';

import * as referenceTable from '@app/models/person';

@Component
({
	selector: 'app-students',
	templateUrl: './students.component.html',
	styleUrls: ['./students.component.css']
})
export class StudentsComponent extends BasePage<Student> implements OnInit
{
	public genderList: Gender[] = referenceTable.GenderList;

	constructor
	(
		private pageDataManagementService: StudentManagementService,
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
			gpa: ['', Validators.required],
			firstName: ['', Validators.required],
			middleName: [''],
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
				error => { console.log('Error at students.component.ts', error); },
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

	edit(item: Student)
	{
		this.selectedItem = item;
		this.setFormValues(this.selectedItem);
	}

	delete(item: Student)
	{
		// Sorry, this is quicker to setup than modal :D
		let result = confirm(`Are you sure you want to delete "${item.firstName} ${item.lastName}"?`);
		if (result)
		{
			this.pageDataManagementService
				.delete(item)
				.subscribe
				(
					data => { this.removeFromList(item); },
					error => { console.log('Error at students.component.ts', error); },
					() => { }
				);
		}
	}

	setFormValues(item: Student)
	{
		this.form.controls['id'].setValue(item.id);
		this.form.controls['gpa'].setValue(item.gpa);
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
		this.selectedItem = this.form.value as Student;

		this.selectedItem.birthDate = this.pickerToDate(this.form.controls['birthDate'].value);

		if (this.selectedItem.id)
		{
			this.pageDataManagementService
				.update(this.selectedItem)
				.subscribe
				(
					data => { this.removeFromList(this.selectedItem); },
					error => { console.log('Error at students.component.ts', error); },
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
					error => { console.log('Error at students.component.ts', error); },
					() => { this.formCancel(); this.loadData(); }
				);
		}
	}
}
