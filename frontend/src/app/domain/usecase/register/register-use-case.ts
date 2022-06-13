import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterGateway } from '../../models/register/gateway/register-gateway';
import { RegisterInput } from '../../models/register/register-input';
import { RegisterOutput } from '../../models/register/register-output';

@Injectable({
  providedIn: 'root'
})

export class RegisterUseCases {
  constructor( private _registerGateWay: RegisterGateway) {}

  register(_data: RegisterInput) : Observable<RegisterOutput> {
    return this._registerGateWay.register(_data);
  }

}
