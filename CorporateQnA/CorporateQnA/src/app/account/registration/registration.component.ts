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
  userImage: string;
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
    designation: [''],
    department: [''],
    jobLocation: [''],
    userImage: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(4)]],

  });

  onPosterChange(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [image] = event.target.files;
      reader.readAsDataURL(image);
      reader.onload = () => {
        this.userImage = reader.result as string;

        this.signInForm.patchValue({
          userImage: this.userImage
        })
      }
    }
  }


  onSubmit() {
    this.service.register(this.signInForm.value).subscribe(
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
