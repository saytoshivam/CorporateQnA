import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { LoginComponent } from './account/login/login.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { CategoryComponent } from './corporate-qn-a/category';
import { CorporateQnAComponent } from './corporate-qn-a/corporate-qn-a.component';
import { HomeComponent } from './corporate-qn-a/home';
import { QuestionComponent } from './question';
import { UserComponent } from './corporate-qn-a/user';

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
      { path: 'questions', component: QuestionComponent },
      { path: 'users/all', component: UserComponent },
      { path: 'categories', component: CategoryComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }