import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountComponent, LoginComponent, RegistrationComponent } from './account';
import { AuthGuard } from './auth/auth.guard';
import { CategoryComponent } from './corporate-qn-a/category';
import { CorporateQnAComponent } from './corporate-qn-a/corporate-qn-a.component';
import { HomeComponent } from './corporate-qn-a/home';
import { UserDetailsComponent, UsersComponent, AllUsersComponent } from './corporate-qn-a/users';

const routes: Routes = [
  { path: '', redirectTo: 'account/login', pathMatch: 'full' },
  {
    path: 'account', component: AccountComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }]
  },
  {
    path: 'qna', component: CorporateQnAComponent, canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomeComponent },
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