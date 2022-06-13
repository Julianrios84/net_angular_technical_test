import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { CategoryGateway } from 'src/app/domain/models/category/gateway/category-gateway';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  public createCategoryForm!: FormGroup;
  public response$!: Observable<void>;

  constructor(public activeModal: NgbActiveModal, private _categoryGateWay: CategoryGateway) { }



  ngOnInit(): void {
    this.createCategoryForm = new FormGroup({
      code: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(10)]),
      title: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(10)]),
      description: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]),
      idParentCategory: new FormControl(0, [Validators.required]),
    })
  }

  get codeField(): AbstractControl | null {
    return this.createCategoryForm.get('code');
  }

  get titleField(): AbstractControl | null {
    return this.createCategoryForm.get('title');
  }

  get descriptionField(): AbstractControl | null {
    return this.createCategoryForm.get('description');
  }

  get parentField(): AbstractControl | null {
    return this.createCategoryForm.get('idParentCategory');
  }

  createCategoryFormSubmit(): void {
    console.log(this.createCategoryForm.value);
    this.response$ = this._categoryGateWay.create(this.createCategoryForm.value);
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
