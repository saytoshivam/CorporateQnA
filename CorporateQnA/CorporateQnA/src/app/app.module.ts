import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';

import { HomeComponent } from './corporate-qn-a/home/home.component';
import { CategoryComponent } from './corporate-qn-a/category/category.component';
import { EditorComponent } from './shared/editor/editor.component';
import { HeaderComponent } from './shared/header/header.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { LoginComponent } from './account/login/login.component';
import { AuthInterceptor } from './auth/auth.intercepter';
import { SectionsNavComponent } from './corporate-qn-a/sections-nav/sections-nav.component';
import { CorporateQnAComponent } from './corporate-qn-a/corporate-qn-a.component';
import { UsersComponent } from './corporate-qn-a/users/users.component';
import { AllUsersComponent } from './corporate-qn-a/users/all-users/all-users.component';
import { UserCardComponent } from './corporate-qn-a/users/user-card/user-card.component';
import { UserDetailsComponent } from './corporate-qn-a/users/user-details/user-details.component';
import { QuestionCardComponent } from './shared/question-card/question-card.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    HomeComponent,
    CategoryComponent,
    EditorComponent,
    HeaderComponent,
    RegistrationComponent,
    LoginComponent,
    SectionsNavComponent,
    CorporateQnAComponent,
    UsersComponent,
    AllUsersComponent,
    UserCardComponent,
    UserDetailsComponent,
    QuestionCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
