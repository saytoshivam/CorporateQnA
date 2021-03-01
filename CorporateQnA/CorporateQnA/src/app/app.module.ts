import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { UserComponent } from './corporate-qn-a/user/user.component';
import { QuestionComponent } from './question/question.component';
import { AnswerComponent } from './question/answer/answer.component';
import { HomeComponent } from './corporate-qn-a/home/home.component';
import { CategoryComponent } from './corporate-qn-a/category/category.component';
import { EditorComponent } from './shared/editor/editor.component';
import { HeaderComponent } from './shared/header/header.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { LoginComponent } from './account/login/login.component';
import { AuthInterceptor } from './auth/auth.intercepter';
import { SectionsNavComponent } from './corporate-qn-a/sections-nav/sections-nav.component';
import { CorporateQnAComponent } from './corporate-qn-a/corporate-qn-a.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    UserComponent,
    QuestionComponent,
    AnswerComponent,
    HomeComponent,
    CategoryComponent,
    EditorComponent,
    HeaderComponent,
    RegistrationComponent,
    LoginComponent,
    SectionsNavComponent,
    CorporateQnAComponent
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
