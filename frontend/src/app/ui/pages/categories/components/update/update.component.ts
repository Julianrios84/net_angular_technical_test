import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { CategoryOutput } from 'src/app/domain/models/category/category-output';
import { CategoryGateway } from 'src/app/domain/models/category/gateway/category-gateway';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {

  @Input() category!: CategoryOutput;

  public updateCategoryForm: FormGroup = new FormGroup({});
  public response$!: Observable<number>;

  constructor(public activeModal: NgbActiveModal, private _categoryGateWay: CategoryGateway) { }

  ngOnInit(): void {
    this.updateCategoryForm = new FormGroup({
      id: new FormControl(this.category.id, [Validators.required]),
      code: new FormControl(this.category.code, [Validators.required, Validators.minLength(2), Validators.maxLength(10)]),
      title: new FormControl(this.category.title, [Validators.required, Validators.minLength(2), Validators.maxLength(10)]),
      description: new FormControl(this.category.description, [Validators.required]),
      idParentCategory: new FormControl(this.category.idParentCategory, [Validators.required]),
    })
  }

  get idField(): AbstractControl | null {
    return this.updateCategoryForm.get('id');
  }

  get codeField(): AbstractControl | null {
    return this.updateCategoryForm.get('code');
  }

  get titleField(): AbstractControl | null {
    return this.updateCategoryForm.get('title');
  }

  get descriptionField(): AbstractControl | null {
    return this.updateCategoryForm.get('description');
  }

  get parentField(): AbstractControl | null {
    return this.updateCategoryForm.get('idParentCategory');
  }

  updateCategoryFormSubmit(): void {
    console.log(this.updateCategoryForm.value);
    this.response$ = this._categoryGateWay.update(this.updateCategoryForm.value.id, this.updateCategoryForm.value);
    this.response$.subscribe({
      next: () => {
        this.activeModal.close(true);
      },
      error: () => {
        this.activeModal.close(false);
      }
    });
  }

}
