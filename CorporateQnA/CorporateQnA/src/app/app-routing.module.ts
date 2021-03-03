import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { LoginComponent } from './account/login/login.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { UsersComponent } from './corporate-qn-a';
import { CategoryComponent } from './corporate-qn-a/category';
import { CorporateQnAComponent } from './corporate-qn-a/corporate-qn-a.component';
import { HomeComponent } from './corporate-qn-a/home';
import { AllUsersComponent } from './corporate-qn-a/users/all-users/all-users.component';
import { UserDetailsComponent } from './corporate-qn-a/users/user-details/user-details.component';
import { UserDetails } from './shared/models';

const routes: Routes = [
  { path: '', redirectTo: 'account/login', pathMatch: 'full' },
  {
    path: 'account', component: AccountComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }]
  },
  {
    path: 'qna', component: CorporateQnAComponent,
    children: [
      { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
      {
        path: 'users', component: UsersComponent,
        children: [
          { path: 'all', component: AllUsersComponent },
          { path: 'view/:id', component: UserDetailsComponent }
        ]
      },
      { path: 'categories', component: CategoryComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }