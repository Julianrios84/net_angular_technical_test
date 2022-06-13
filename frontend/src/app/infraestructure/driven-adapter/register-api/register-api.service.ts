import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterGateway } from 'src/app/domain/models/register/gateway/register-gateway';
import { RegisterInput } from 'src/app/domain/models/register/register-input';
import { RegisterOutput } from 'src/app/domain/models/register/register-output';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterApiService extends RegisterGateway{

  private _url = environment.url;
  constructor(private http: HttpClient) { super(); }

  register(_data: RegisterInput): Observable<RegisterOutput> {
    return this.http.post<RegisterOutput>(`/user/register`, _data);
  }
}
