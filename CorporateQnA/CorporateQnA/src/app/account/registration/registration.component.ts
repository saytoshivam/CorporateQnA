import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/shared/services/account.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {
  constructor(private fb: FormBuilder, public service: AccountService, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    this.signInForm.reset();
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/home');
  }
  signInForm = this.fb.group({
    userName: ['', Validators.required],
    email: ['', Validators.email],
    fullName: [''],

    passwords: this.fb.group({
      password: ['', [Validators.required, Validators.minLength(4)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('confirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);

    }
    return null;
  }

  onSubmit() {
    this.service.register(this.signInForm).subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.signInForm.reset();
          this.toastr.success('New user created!', 'Registration successful.');
        } else {
          res.errors.forEach((element: { code: any; description: string | undefined; }) => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is already taken', 'Registration failed.');
                break;

              default:
                this.toastr.error(element.description, 'Registration failed.');
                break;
            }
          });
        }
      },
      err => {
        alert(err);
      }
    );
  }

}
