import { Observable } from 'rxjs';
import { CategoryOutput } from '../category-output';
import { CategoryInput } from '../category-input';

export abstract class CategoryGateway {
    abstract getByID(id: number): Observable<CategoryOutput>;
    abstract getAll(): Observable<Array<CategoryOutput>>;
    abstract create (_data :CategoryInput) : Observable<void>;
    abstract update (id: number, _data :CategoryInput) : Observable<number>;
    abstract delete (id: number) : Observable<void>;
}
