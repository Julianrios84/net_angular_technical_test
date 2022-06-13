import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryInput } from 'src/app/domain/models/category/category-input';
import { CategoryOutput } from 'src/app/domain/models/category/category-output';
import { CategoryGateway } from 'src/app/domain/models/category/gateway/category-gateway';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService extends CategoryGateway {

  private _url = environment.url;

  constructor(private http: HttpClient) { super(); }

  getByID(id: number): Observable<CategoryOutput> {
    return this.http.get<CategoryOutput>(`/category/${id}`);
  }
  getAll(): Observable<CategoryOutput[]> {
    return this.http.get<CategoryOutput[]>(`/category`);
  }
  create(_data: CategoryInput): Observable<void> {
    return this.http.post<void>(`/category`, _data);
  }
  update(id: number, _data: CategoryInput): Observable<number> {
    return this.http.put<number>(`/category/${id}`, _data);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`/category/${id}`);
  }

}
