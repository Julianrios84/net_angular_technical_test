import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginGateway } from '../../models/login/gateway/login-gateway';
import { LoginInput } from '../../models/login/login-input';
import { LoginOutput } from '../../models/login/login-output';

@Injectable({
  providedIn: 'root'
})

export class LoginUseCases {
  constructor( private _loginGateWay: LoginGateway) {}

  login(_data: LoginInput) : Observable<LoginOutput> {
    return this._loginGateWay.login(_data);
  }

}
