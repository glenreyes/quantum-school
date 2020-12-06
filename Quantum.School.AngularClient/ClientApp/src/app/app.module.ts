import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { environment } from '@environment';

import { AppRoutingModule } from '@app/.';
import { AppComponent } from '@app/.';
import { NavMenuComponent } from '@app/components';

import { PipesModule } from '@app/pipes';

import
{
	HomeComponent,
	CounterComponent,
	FetchDataComponent,
	ClassSchedulesComponent,
	SubjectsComponent,
	TeachersComponent,
	StudentsComponent
} from '@app/pages';

import
{
	ClassScheduleManagementService,
	SubjectManagementService,
	TeacherManagementService,
	StudentManagementService
} from '@app/services';

@NgModule
({
	imports:
	[
		BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
		HttpClientModule,
		ReactiveFormsModule,
		FormsModule,
		AppRoutingModule,
		PipesModule,
		NgbModule
	],
	declarations:
	[
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		CounterComponent,
		FetchDataComponent,
		ClassSchedulesComponent,
		SubjectsComponent,
		TeachersComponent,
		StudentsComponent
	],
	providers:
	[
		ClassScheduleManagementService,
		SubjectManagementService,
		TeacherManagementService,
		StudentManagementService
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
