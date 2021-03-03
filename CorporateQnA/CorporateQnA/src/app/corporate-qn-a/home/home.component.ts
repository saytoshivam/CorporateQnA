import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { faCompressAlt, faExpandAlt, faPlus, faRedo, faSearch } from '@fortawesome/free-solid-svg-icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { debounceTime } from 'rxjs/operators';
import { Category, QuestionDetails } from 'src/app/shared/models';
import { Question } from 'src/app/shared/models/question.model';
import { SearchFilterModel } from 'src/app/shared/models/search-filter.model';
import { CategoryService, QuestionService } from 'src/app/shared/services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent implements OnInit {

  //Icons
  faSearch = faSearch;
  faPlus = faPlus;
  faRedo = faRedo;
  faExpandAlt = faExpandAlt
  faCompressAlt = faCompressAlt

  //Form groups
  searchForm: FormGroup;
  newAnswer: FormGroup;
  newQuestionForm: FormGroup;

  //Modal controls
  toggleFlyoutEditor = false;
  modalRef: BsModalRef;

  //Current User
  user: any

  //Models
  currentQuestion: QuestionDetails;
  categoryOptions: Category[] = []
  allQuestions: QuestionDetails[] = []

  constructor(private modalService: BsModalService, private categoryService: CategoryService, private questionService: QuestionService) {

      this.searchForm = new FormGroup({
          searchInput: new FormControl(""),
          categoryId: new FormControl(0),
          show: new FormControl(0),
          sortBy: new FormControl(0)
      })

      this.newQuestionForm = new FormGroup({
          title: new FormControl("", [Validators.required]),
          content: new FormControl("", [Validators.required, this.editorValidator()]),
          questionCategory: new FormControl(0, [Validators.required, this.categoryIdValidator()])
      })

      this.newAnswer = new FormGroup({
          content: new FormControl("", [Validators.required]),
      })
  }

  ngOnInit() {
      this.categoryService.getAllCategories().subscribe(categories => {
          this.categoryOptions = [...this.categoryOptions, ...categories]
      })

      
          this.user=null;

      this.questionService.getAllQuestions().subscribe(questions => {
          this.allQuestions = [...questions];
      });

      this.searchForm.valueChanges.pipe(debounceTime(420)).subscribe((filter: SearchFilterModel) => {

          filter.userId = this.user['userId']
          filter.categoryId = Number(filter.categoryId);
          filter.show = Number(filter.show);
          filter.sortBy = Number(filter.sortBy)

          this.questionService.searchQuestion(filter).subscribe(questions => {
              this.allQuestions = questions;
              this.currentQuestion = null;
          })
      })
  }

  openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template, { class: "custom-modal" });
  }

  createQuestion() {
      let askedBy = this.user['userId']
      let categoryId = this.newQuestionForm.get("questionCategory").value;
      let content = this.removeTags(this.newQuestionForm.get("content").value);
      let title = this.newQuestionForm.get("title").value;

      let question: Question = new Question({ askedBy, categoryId, content, title })

      this.questionService.postQuestion(question).subscribe(value => {
          let questionData = new QuestionDetails({
              id: value,
              userName: this.user['userName'],
              title: title,
              content,
              askedBy,
              categoryId,
              upvoteCount: 0,
              viewCount: 0,
              isResolved: 0,
              answerCount: 0
          });

          this.allQuestions.push(questionData)
          this.modalRef.hide();
      })
  }

  viewQuestion(question: QuestionDetails) {
      this.currentQuestion = question;
  }

  removeTags(str) {
      if ((str === null) || (str === ''))
          return false;
      else
          str = str.toString();

      // Regular expression to identify HTML tags in  
      // the input string. Replacing the identified  
      // HTML tag with a null string. 
      return str.replace(/(<([^>]+)>)/ig, '');
  }

  resetSearch() {
      this.searchForm.get("searchInput").patchValue("")
      this.searchForm.get("categoryId").patchValue(0)
      this.searchForm.get("show").patchValue(0)
      this.searchForm.get("sortBy").patchValue(0)
  }

  resetNewQuestion(){
      this.newQuestionForm.reset();
      this.newQuestionForm.get("content").patchValue("")
      this.newQuestionForm.get("questionCategory").patchValue(0);
      this.modalRef.hide()
  }

  categoryIdValidator(): ValidatorFn {
      return (control: AbstractControl): { [key: string]: any } | null => {
          return control.value == 0 ? { "categoryId": "invalid category id" } : null;
      };
  }

  editorValidator(): ValidatorFn {
      return (control: AbstractControl): { [key: string]: any } | null => {
          let empty = this.removeTags(control.value).length == 0

          return empty ? { "empty": "Empty content" } : null;
      };
  }

}
