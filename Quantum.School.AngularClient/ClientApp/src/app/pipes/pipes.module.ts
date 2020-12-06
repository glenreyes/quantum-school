import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule }        from '@angular/common';

// Common Components
import { EllipsizePipe } from './ellipsize.pipe';

@NgModule
({
	imports:
	[
		CommonModule
	],
	declarations:
	[
		EllipsizePipe
	],
	exports:
	[
		EllipsizePipe
	]
})

export class PipesModule
{

}
