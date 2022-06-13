import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { CategoriesComponent } from './categories.component';
import { HeaderModule } from '../../common/header/header.module';
import { FooterModule } from '../../common/footer/footer.module';
import { CreateComponent } from './components/create/create.component';
import { UpdateComponent } from './components/update/update.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CategoriesComponent,
    CreateComponent,
    UpdateComponent
  ],
  imports: [
    CommonModule,
    CategoriesRoutingModule,
    HeaderModule,
    FooterModule,
    ReactiveFormsModule
  ]
})
export class CategoriesModule { }
