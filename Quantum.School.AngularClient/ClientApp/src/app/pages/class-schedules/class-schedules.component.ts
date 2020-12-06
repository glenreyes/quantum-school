// TODO:
// Sorry, I should have broken down this page into smaller components.

import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription, forkJoin } from 'rxjs';
import { ClassScheduleManagementService, SubjectManagementService, TeacherManagementService, StudentManagementService } from '@app/services';
import { ClassSchedule, ClassScheduleEntry, Subject, Teacher, Student } from '@app/models';
import { BasePage } from '../base-page.component';

@Component
({
	selector: 'app-class-schedules',
	templateUrl: './class-schedules.component.html',
	styleUrls: ['./class-schedules.component.css']
})
export class ClassSchedulesComponent extends BasePage<ClassSchedule> implements OnInit
{
	// These are for reference lists
	public subjectList: Subject[];
	public teacherList: Teacher[];
	public studentList: Student[];

	public entryItem: ClassScheduleEntry;
	public highlightedItem: ClassSchedule;
	public classStudents: Student[];
	public availableStudents: Student[];
	public studentForm: FormGroup;

	public classStudentsEditMode: boolean = false;

	constructor
	(
		private pageDataManagementService: ClassScheduleManagementService,
		private subjectManagementService: SubjectManagementService,
		private teacherManagementService: TeacherManagementService,
		private studentManagementService: StudentManagementService,
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
			location: ['', Validators.required],
			subject: ['', Validators.required],
			teacher: ['', Validators.required]
		});

		this.studentForm =this.formBuilder.group
		({
			id: [],
			gpa: [],
			firstName: [],
			middleName: [],
			lastName: [],
			gender: [],
			birthDate: []
		});
	}

	loadData()
	{
		this.isLoading = true;
		// this.pageDataManagementService
		// 	.getAll()
		// 	.subscribe
		// 	(
		// 		data  => { this.list = data; },
		// 		error => { console.log('Error at class-schedules.component.ts', error); },
		// 		() => { this.isLoading = false; }
		// 	);

		forkJoin
		({
			modelData: this.pageDataManagementService.getAll(),
			subjects: this.subjectManagementService.getAll(),
			teachers: this.teacherManagementService.getAll(),
			students: this.studentManagementService.getAll()
		})
		.subscribe(({modelData, subjects, teachers, students}) =>
		{
			this.list = modelData;
			this.subjectList = subjects;
			this.teacherList = teachers;
			this.studentList = students;

			// If list is empty, unselect
			// If item is not the list anymore, select first item
			if (this.list.length === 0)
				this.highlightedItem = undefined;
			else if(!this.list.includes(this.highlightedItem))
				this.highlightedItem = this.list[0];

			if (this.highlightedItem)
				this.loadClassStudents(this.highlightedItem.id);

			this.isLoading = false;
		});
	}

	add()
	{
		this.selectedItem =
		{
			id: undefined,
			location: '',
			subject: {} as Subject,
			teacher: {} as Teacher,
			students: []
		};
		this.setFormValues(this.selectedItem);
	}

	edit(item: ClassSchedule)
	{
		this.selectedItem = item;
		this.setFormValues(this.selectedItem);
	}

	delete(item: ClassSchedule)
	{
		// Sorry, this is quicker to setup than modal :D
		let teacherName = `${item.teacher.title} ${item.teacher.firstName} ${item.teacher.lastName}`;
		let result = confirm(`Are you sure you want to delete the class schedule "${item.subject.name} @ ${item.location} with ${teacherName}"?`);
		if (result)
		{
			this.pageDataManagementService
				.delete(item.id)
				.subscribe
				(
					data => { this.removeFromList(item); },
					error => { console.log('Error at class-schedules.component.ts', error); },
					() => { }
				);
		}
	}

	setFormValues(item: ClassSchedule)
	{
		this.form.controls['id'].setValue(item.id);
		this.form.controls['location'].setValue(item.location);
		this.form.controls['subject'].setValue(item.subject.id);
		this.form.controls['teacher'].setValue(item.teacher.id);
	}

	formSubmit()
	{
		let id:string = this.form.value.id;
		this.entryItem = this.form.value as ClassScheduleEntry;
		if (id)
		{
			this.pageDataManagementService
				.update(id, this.entryItem)
				.subscribe
				(
					data => { this.removeFromList(this.selectedItem); },
					error => { console.log(`Error at class-schedules.component.ts`, error); },
					() => { this.formCancel(); this.loadData(); }
				);
		}
		else
		{
			this.pageDataManagementService
				.add(this.entryItem)
				.subscribe
				(
					data => { this.removeFromList(this.selectedItem); },
					error => { console.log(`Error at class-schedules.component.ts`, error); },
					() => { this.formCancel(); this.loadData(); }
				);
		}
	}

	highlight(item: ClassSchedule)
	{
		this.highlightedItem = item;
		console.log(this.highlightedItem);

		this.loadClassStudents(this.highlightedItem.id);
	}

	loadClassStudents(classScheduleId: string)
	{
		this.pageDataManagementService
			.getStudents(classScheduleId)
			.subscribe
			(
				data  => { this.classStudents = data; },
				error => { console.log('Error at class-schedules.component.ts', error); },
				() =>
				{
					// Remove existing students in selection when adding to class
					this.availableStudents = this.studentList.filter(i =>
						!this.classStudents.some(cs => cs.id === i.id));
				}
			);
	}

	showClassStudentForm()
	{
		this.updateClassStudentForm(this.availableStudents[0].id);
		this.classStudentsEditMode = true;
	}

	cancelClassStudentForm()
	{
		this.classStudentsEditMode = false;
	}

	addClassStudent(classScheduleId: string, studentId: string)
	{
		this.pageDataManagementService
			.addStudent(classScheduleId, studentId)
			.subscribe
			(
				data  => { this.loadClassStudents(this.highlightedItem.id); },
				error => { console.log('Error at class-schedules.component.ts', error); },
				() => {  }
			);
	}

	removeClassStudent(classScheduleId: string, studentId: string)
	{
		this.pageDataManagementService
			.removeStudent(classScheduleId, studentId)
			.subscribe
			(
				data  =>
				{
					let index = this.classStudents.findIndex(x => x.id === studentId);
					console.log(this.classStudents);
					console.log(index);

					if (index > -1)
						this.classStudents.splice(index, 1);
				},
				error => { console.log('Error at class-schedules.component.ts', error); },
				() => {  }
			);
	}

	updateClassStudentForm(studentId: string)
	{
		console.log(studentId);
		let student: Student = this.studentList.find(x => x.id === studentId);
		this.studentForm.controls['id'].setValue(student.id);
		this.studentForm.controls['gpa'].setValue(student.gpa);
		this.studentForm.controls['firstName'].setValue(student.firstName);
		this.studentForm.controls['middleName'].setValue(student.middleName);
		this.studentForm.controls['lastName'].setValue(student.lastName);
		this.studentForm.controls['gender'].setValue(student.gender);
		this.studentForm.controls['birthDate'].setValue(new Date(student.birthDate).toLocaleDateString());
	}

	studentFormSubmit()
	{
		let classScheduleId:string = this.highlightedItem.id;
		let studentId:string = this.studentForm.value.id;
		let student: Student = this.studentList.find(x => x.id === studentId);

		let fullName:string = `${student.firstName} ${student.lastName}`;
		let lastNameCaomparer:string = student.lastName.toLowerCase();

		// Check if matching surname (business rule)
		if (this.classStudents.some(cs => cs.lastName.toLowerCase() === lastNameCaomparer))
		{
			// Too lazy to create a modal popup
			alert(`Sorry, somehow this class can't add ${fullName} because there's already someone with the same surname "${student.lastName}" attending that class. Weird huh?`);
			return;
		}

		this.pageDataManagementService
			.addStudent(classScheduleId, studentId)
			.subscribe
			(
				data => { this.loadClassStudents(classScheduleId); },
				error => { console.log(`Error at class-schedules.component.ts`, error); },
				() => { this.cancelClassStudentForm(); }
			);

	}
}
