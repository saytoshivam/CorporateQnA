import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { LoginComponent } from './account/login/login.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { CategoryComponent } from './corporate-qn-a/category';
import { HomeComponent } from './corporate-qn-a/home';
import { QuestionComponent } from './question';
import { UserComponent } from './user';

const routes: Routes = [
  { path: '', redirectTo: 'questions', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'questions', component: QuestionComponent },
  { path: 'users/all', component: UserComponent },
  { path: 'categories', component: CategoryComponent },
  {
    path: 'account', component: AccountComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }