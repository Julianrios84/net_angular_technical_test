import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterGateway } from 'src/app/domain/models/register/gateway/register-gateway';
import { RegisterOutput } from 'src/app/domain/models/register/register-output';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public registerForm!: FormGroup;
  public response$!: Observable<RegisterOutput>;
  public data!: RegisterOutput;

  constructor(private _registerGateWay: RegisterGateway,  private _router: Router) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstname: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      username: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    })
  }

  get firstNameField(): any {
    return this.registerForm.get('firstname');
  }

  get lastNameField(): any {
    return this.registerForm.get('lastname');
  }

  get userNameField(): any {
    return this.registerForm.get('username');
  }

  get emailField(): any {
    return this.registerForm.get('email');
  }
  get passwordField(): any {
    return this.registerForm.get('password');
  }

  registerFormSubmit(): void {
    console.log(this.registerForm.value);
    // Call Api
    this.response$ = this._registerGateWay.register(this.registerForm.value);
    this.response$.subscribe((data: RegisterOutput) => {
      console.log(data);
      localStorage.setItem('token', data.token);
      this._router.navigateByUrl('/categories');
    });
  }

}
