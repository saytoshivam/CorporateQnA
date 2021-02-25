import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoriesComponent } from './categories/categories.component';
import { AccountComponent } from './account/account.component';
import { UserComponent } from './user/user.component';
import { QuestionComponent } from './question/question.component';
import { AnswerComponent } from './question/answer/answer.component';
import { HomeComponent } from './home/home.component';
import { CategoryComponent } from './category/category.component';
import { EditorComponent } from './shared/editor/editor.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoriesComponent,
    AccountComponent,
    UserComponent,
    QuestionComponent,
    AnswerComponent,
    HomeComponent,
    CategoryComponent,
    EditorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
