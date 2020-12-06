import { BaseModel } from './base-model';

export interface Subject extends BaseModel
{
	name: string;
	description?: string;
}
