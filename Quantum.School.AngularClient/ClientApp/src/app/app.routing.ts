import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

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

const routes: Routes =
[
	{ path: '', component: HomeComponent, pathMatch: 'full' },
	{ path: 'counter', component: CounterComponent },
	{ path: 'fetch-data', component: FetchDataComponent },

	{ path: 'class-schedules', component: ClassSchedulesComponent },
	{ path: 'subjects', component: SubjectsComponent },
	{ path: 'teachers', component: TeachersComponent },
	{ path: 'students', component: StudentsComponent },

	/*{ path: '', loadChildren: '@app/main/main.module#MainModule' },*/
	{ path: '**', redirectTo: '' }
];

@NgModule
({
	imports:
	[
		RouterModule.forRoot(routes)
	],
	exports: [RouterModule]
})
export class AppRoutingModule { }
