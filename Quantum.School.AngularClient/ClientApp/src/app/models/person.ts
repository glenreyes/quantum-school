import { BaseModel } from './base-model';

export interface Person extends BaseModel
{
	firstName?: string;
	middleName?: string;
	lastName?: string;

	birthDate?: Date;
	gender?: string;
}

export interface Gender
{
	id: string;
	name: string;
}

export interface NameTitle
{
	id: string;
	name: string;
}

// I cheated, this should be retrieved from the API instead.
export const NameTitleList: NameTitle[] =
[
	{id: '',   name: ''},
	{id: 'Mr',   name: 'Mr'},
	{id: 'Ms',   name: 'Ms'},
	{id: 'Mrs',  name: 'Mrs'},
	{id: 'Engr', name: 'Engr'}
];
// Same here as well
export const GenderList: Gender[] =
[
	{id: '',  name: 'Not Specified'},
	{id: 'M', name: 'Male'},
	{id: 'F', name: 'Female'}
];
