import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginGateway } from 'src/app/domain/models/login/gateway/login-gateway';
import { LoginOutput } from 'src/app/domain/models/login/login-output';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm!: FormGroup;
  public response$!: Observable<LoginOutput>;
  public data!: LoginOutput;

  constructor(private _loginGateWay: LoginGateway, private _router: Router) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    })
  }

  get emailField(): any {
    return this.loginForm.get('email');
  }
  get passwordField(): any {
    return this.loginForm.get('password');
  }

  loginFormSubmit(): void {
    console.log(this.loginForm.value);
    // Call Api
    this.response$ = this._loginGateWay.login(this.loginForm.value);
    this.response$.subscribe((data: LoginOutput) => {
      console.log(data);
      localStorage.setItem('token', data.token);
      this._router.navigateByUrl('/categories');
    });
  }
}
