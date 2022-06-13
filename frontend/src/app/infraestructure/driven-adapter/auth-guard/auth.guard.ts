import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private _router: Router) {

  }

  canActivate( _route: ActivatedRouteSnapshot, _state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');

    if(!token) {
      this._router.navigateByUrl('/');
    }

    return !!token;
  }

}
