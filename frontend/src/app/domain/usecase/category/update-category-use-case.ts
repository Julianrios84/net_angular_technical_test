import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryInput } from '../../models/category/category-input';
import { CategoryGateway } from '../../models/category/gateway/category-gateway';

@Injectable({
  providedIn: 'root'
})

export class UpdateCategoryUseCases {
  constructor( private _categoryGateWay: CategoryGateway) {}

  updateCategory(id:number, _data: CategoryInput) : Observable<number> {
    return this._categoryGateWay.update(id, _data);
  }

}
