import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { Category, QuestionDetails, Question } from 'src/app/shared/models';
import { AccountService, CategoryService, QuestionService } from 'src/app/shared/services';
import { redoIcon, plusIcon, searchIcon, expandAltIcon, compressAltIcon } from '../../shared/constants';
@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styles: [
    ]
})
export class HomeComponent implements OnInit {

    //Icons
    faSearch = searchIcon
    faPlus = plusIcon
    faRedo = redoIcon
    faExpandAlt = expandAltIcon
    faCompressAlt = compressAltIcon

    //Form groups
    searchForm: FormGroup;
    newAnswer: FormGroup;
    newQuestionForm: FormGroup;

    //Modal controls
    toggleFlyoutEditor = false;
    modalRef: BsModalRef;

    //Logged In UserId
    loggedInUserID: number
    userName: string

    //Models
    currentQuestion: QuestionDetails;
    categoryOptions: Category[] = []
    filteredQuestions: QuestionDetails[] = []
    searchInputValue: string;
    selectedCategory: number;
    selectedShow: number;
    selectedSortBy: number;
    questions: QuestionDetails[] = []

    constructor(private accountService: AccountService, private modalService: BsModalService, private categoryService: CategoryService, private questionService: QuestionService) {

        this.searchForm = new FormGroup({
            searchInput: new FormControl(""),
            categoryId: new FormControl(0),
            show: new FormControl(0),
            sortBy: new FormControl(0)
        })

        this.newQuestionForm = new FormGroup({
            title: new FormControl("", [Validators.required]),
            content: new FormControl("", [Validators.required, this.editorValidator()]),
            questionCategory: new FormControl(0, [Validators.required])
        })

        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required]),
        })
    }

    ngOnInit() {
        this.categoryService.getAllCategories().subscribe(categories => {
            this.categoryOptions = [...this.categoryOptions, ...categories]
        })

        this.loggedInUserID = this.accountService.getUserId();
        this.userName = this.accountService.getUserName();

        this.questionService.getAllQuestions().subscribe(questions => {
            this.questions = <QuestionDetails[]>questions;
            this.filteredQuestions = questions;
        });

        this.searchForm.get("searchInput").valueChanges.subscribe(input => {
            this.searchInputValue = input;
            this.filterQuestions();
        })
        this.searchForm.get("categoryId").valueChanges.subscribe(value => {
            this.selectedCategory = value;
            this.filterQuestions();
        })
        this.searchForm.get("show").valueChanges.subscribe(value => {
            this.selectedShow = value;
            this.filterQuestions();
        })
        this.searchForm.get("sortBy").valueChanges.subscribe(value => {
            this.selectedSortBy = value;
            this.filterQuestions();
        })

    }

    filterQuestions() {

        //filter on input value
        this.filteredQuestions = this.questions.filter(ques => ques.title.toLowerCase().indexOf(this.searchInputValue ?? "".toLowerCase()) !== -1);

        //filter on category dropdown
        if (this.selectedCategory == 0)
            this.filteredQuestions = this.filteredQuestions;
        else
            this.filteredQuestions = this.filteredQuestions.filter(ques => ques.categoryId == this.selectedCategory);


        //filter on show value
        if (this.selectedShow == 0) {
            this.filteredQuestions = this.filteredQuestions
        }
        else if (this.selectedShow == 1) {
            this.filteredQuestions = this.filteredQuestions.filter(ques => ques.askedBy == this.loggedInUserID);
        }
        else if (this.selectedShow == 3) {
            this.filteredQuestions = this.filteredQuestions.sort(
                function (a, b) {
                    return b.viewCount - a.viewCount;
                }).slice(0, 5);
        }
        else if (this.selectedShow == 2) {
            this.filteredQuestions = this.filteredQuestions.filter(ques => ques.askedBy == this.loggedInUserID);
        }
        else if (this.selectedShow == 4) {
            this.filteredQuestions = this.filteredQuestions.filter(ques => ques.isResolved == true)
        }
        else if (this.selectedShow == 5) {
            this.filteredQuestions = this.filteredQuestions.filter(ques => ques.isResolved == false)
        }

        //filter Sort by :time
        if (this.selectedSortBy == 0) {
            this.filteredQuestions = this.filteredQuestions
        }
        else if (this.selectedSortBy == 2) {
            this.filterQuestionsBasesOnDaysCount(2);
        }
        else if (this.selectedSortBy == 10) {
            this.filterQuestionsBasesOnDaysCount(10);
        }
        else if (this.selectedSortBy == 30) {
            this.filterQuestionsBasesOnDaysCount(30);
        }
    }

    filterQuestionsBasesOnDaysCount(noOfDays: number) {
        this.filteredQuestions = this.filteredQuestions.filter(ques => moment(new Date()).diff(ques.askedOn, 'days') < noOfDays);

    }


    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: "custom-modal" });
    }

    postQuestion() {
        let askedBy = this.loggedInUserID
        let categoryId = this.newQuestionForm.get("questionCategory").value;
        let description = this.removeTags(this.newQuestionForm.get("content").value);
        let title = this.newQuestionForm.get("title").value;

        let question: Question = new Question({ askedBy, categoryId, description, title })

        this.questionService.postQuestion(question).subscribe(value => {
            let questionData = new QuestionDetails({
                id: value,
                title,
                description,
                askedBy,
                categoryId,
                upvoteCount: 0,
                viewCount: 0,
                isResolved: 0,
                answerCount: 0
            });

            this.questions.push(questionData)
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

    resetNewQuestion() {
        this.newQuestionForm.reset();
        this.newQuestionForm.get("content").patchValue("")
        this.newQuestionForm.get("questionCategory").patchValue(0);
        this.modalRef.hide()
    }

    editorValidator(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null => {
            let empty = this.removeTags(control.value).length == 0

            return empty ? { "empty": "Empty content" } : null;
        };
    }

}
