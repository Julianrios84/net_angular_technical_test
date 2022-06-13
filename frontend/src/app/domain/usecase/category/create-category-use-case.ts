import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryInput } from '../../models/category/category-input';
import { CategoryGateway } from '../../models/category/gateway/category-gateway';

@Injectable({
  providedIn: 'root'
})

export class CreateCategoryUseCases {
  constructor( private _categoryGateWay: CategoryGateway) {}

  createCategory(_data: CategoryInput) : Observable<void> {
    return this._categoryGateWay.create(_data);
  }

}
