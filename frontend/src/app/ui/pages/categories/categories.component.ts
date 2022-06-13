import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs'
import { CategoryOutput } from 'src/app/domain/models/category/category-output';
import { CategoryGateway } from 'src/app/domain/models/category/gateway/category-gateway';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { CreateComponent } from './components/create/create.component';
import { UpdateComponent } from './components/update/update.component';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  public categories$!: Observable<CategoryOutput[]>;

  constructor(private _categoryGateWay: CategoryGateway, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadData();
  }

  createOpen() {
    const modalRef = this.modalService.open(CreateComponent);
    modalRef.result.then((result) => {
      console.log(result);
      this.loadData();
    }, (reason) => {
      console.log(reason)
    });
  }

  updateOpen(category: CategoryOutput) {
    const modalRef = this.modalService.open(UpdateComponent);
    modalRef.componentInstance.category = category;

    modalRef.result.then((result) => {
      console.log(result);
      this.loadData();
    }, (err) => {
      console.log(err)
    });
  }

  private loadData() {
    this.categories$ = this._categoryGateWay.getAll();
  }

  deleteCategory(id: number) {
    this._categoryGateWay.delete(id).subscribe({
      next: () => {
        this.loadData();
      }
    });
  }

}
