import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppComponent } from './app.component';
import { NgxEditorModule } from 'ngx-editor';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgSelectModule } from '@ng-select/ng-select';

import { HomeComponent } from './corporate-qn-a/home';
import { CategoryComponent } from './corporate-qn-a/category';
import { EditorComponent } from './shared/editor';
import { HeaderComponent } from './shared/header';
import { AccountComponent, RegistrationComponent, LoginComponent } from './account';
import { AuthInterceptor } from './auth';
import { SectionsNavComponent } from './corporate-qn-a/sections-nav';
import { CorporateQnAComponent } from './corporate-qn-a';
import { UsersComponent, AllUsersComponent, UserDetailsComponent } from './corporate-qn-a/users';
import { UserCardComponent } from './corporate-qn-a/users/user-card/user-card.component';
import { QuestionCardComponent } from './shared/question-card/question-card.component';
import { AnswerPanelComponent } from './shared/answer-panel/answer-panel.component';
import { AnswerComponent } from './shared/answer/answer.component';
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
    QuestionCardComponent,
    AnswerPanelComponent,
    AnswerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgxEditorModule,
    FontAwesomeModule,
    NgSelectModule,
    ModalModule.forChild()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
