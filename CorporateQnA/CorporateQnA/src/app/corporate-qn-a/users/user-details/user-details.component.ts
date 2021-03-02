import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { QuestionDetails, UserDetails } from 'src/app/shared/models';
import { EventService, QuestionService, UserService } from 'src/app/shared/services';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styles: [
  ]
})
export class UserDetailsComponent implements OnInit {
  // faArrowLeft = faArrowLeft
  // thumbsUp = faThumbsUp
  // thumbsDown = faThumbsDown
  userDetails: UserDetails;
  userQuestions: QuestionDetails[] = []
  userAnsweredQuestions: QuestionDetails[] = []
  currentQuestion: QuestionDetails

  currentTab: string = "ALL"
  currentQuestions: QuestionDetails[] = []

  constructor(private eventService: EventService, private questionService: QuestionService, private router: Router, private activatedRoute: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(routeId => {
      this.questionService.getQuestionsByUserId(Number(routeId['id'])).subscribe(res => {
        this.userQuestions = <QuestionDetails[]>res
      });
      this.userAnsweredQuestions = null;

      this.currentQuestions = this.userQuestions

      this.eventService.on<UserDetails>().subscribe(res => {
        this.userDetails = res
        console.log("destination" + this.userDetails.fullName);
      })

    })
  }

  setTab(tab) {
    if (tab == "ALL") {
      this.currentQuestions = this.userQuestions;
    } else {
      this.currentQuestions = this.userAnsweredQuestions;
    }
    this.currentTab = tab;
    this.currentQuestion = null;
  }

  viewQuestion(i: QuestionDetails) {
    this.currentQuestion = i
  }

}
