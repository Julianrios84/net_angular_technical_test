import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryGateway } from '../../models/category/gateway/category-gateway';

@Injectable({
  providedIn: 'root'
})

export class DeleteCategoryUseCases {
  constructor( private _categoryGateWay: CategoryGateway) {}

  deleteCategory(id: number) : Observable<void> {
    return this._categoryGateWay.delete(id);
  }

}
