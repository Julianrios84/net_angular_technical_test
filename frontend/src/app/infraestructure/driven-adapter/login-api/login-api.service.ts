import { Injectable } from '@angular/core';
import { LoginGateway } from 'src/app/domain/models/login/gateway/login-gateway';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginInput } from 'src/app/domain/models/login/login-input';
import { LoginOutput } from 'src/app/domain/models/login/login-output';

@Injectable({
  providedIn: 'root'
})
export class LoginApiService extends LoginGateway {

  private _url = environment.url;

  constructor(private http: HttpClient) { super(); }

  login(_data: LoginInput): Observable<LoginOutput> {
    return this.http.post<LoginOutput>(`/user/login`, _data);
  }
}
