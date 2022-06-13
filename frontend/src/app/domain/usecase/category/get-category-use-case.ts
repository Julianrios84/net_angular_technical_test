import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryOutput } from '../../models/category/category-output';
import { CategoryGateway } from '../../models/category/gateway/category-gateway';

@Injectable({
  providedIn: 'root'
})

export class GetAlbumUseCases {
  constructor( private _categoryGateWay: CategoryGateway) {}
  getCategoryById (id: number) : Observable <CategoryOutput> {
    //TODO: En este sitio podríamos manejar las configuraciones
    //en cache
    return this._categoryGateWay.getByID(id);
  }
  getAllCategory () : Observable <Array<CategoryOutput>> {
    return this._categoryGateWay.getAll();
  }

}
