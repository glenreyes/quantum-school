import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { SubjectManagementService } from '@app/services';
import { Subject } from '@app/models';
import { BasePage } from '../base-page.component';

@Component
({
	selector: 'app-subjects',
	templateUrl: './subjects.component.html',
	styleUrls: ['./subjects.component.css']
})
export class SubjectsComponent extends BasePage<Subject> implements OnInit
{
	constructor
	(
		private pageDataManagementService: SubjectManagementService,
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
			name: [null, Validators.required],
			description: [null]
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
				error => { console.log('Error at subjects.component.ts', error); },
				() => { this.isLoading = false; }
			);
	}

	add()
	{
		this.selectedItem =
		{
			id: '',
			name: ''
		};
		this.setFormValues(this.selectedItem);
	}

	edit(item: Subject)
	{
		this.selectedItem = item;
		this.setFormValues(this.selectedItem);
	}

	delete(item: Subject)
	{
		// Sorry, this is quicker to setup than modal :D
		let result = confirm(`Are you sure you want to delete the subject "${item.name}"?`);
		if (result)
		{
			this.pageDataManagementService
				.delete(item)
				.subscribe
				(
					data => { this.removeFromList(item); },
					error => { console.log('Error at subjects.component.ts', error); },
					() => { }
				);
		}
	}

	setFormValues(item: Subject)
	{
		this.form.controls['id'].setValue(item.id);
		this.form.controls['name'].setValue(item.name);
		this.form.controls['description'].setValue(item.description);
	}

	formSubmit()
	{
		this.selectedItem = this.form.value as Subject;

		if (this.selectedItem.id)
		{
			this.pageDataManagementService
				.update(this.selectedItem)
				.subscribe
				(
					data => { this.removeFromList(this.selectedItem); },
					error => { console.log(`Error at subjects.component.ts`, error); },
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
					error => { console.log(`Error at subjects.component.ts`, error); },
					() => { this.formCancel(); this.loadData(); }
				);
		}
	}
}
