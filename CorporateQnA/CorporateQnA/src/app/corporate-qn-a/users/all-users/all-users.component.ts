import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { faSearch, faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { UserDetails } from 'src/app/shared/models';
import { EventService, UserService } from 'src/app/shared/services';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styles: []
})
export class AllUsersComponent implements OnInit {
  searchForm: FormGroup;
   faSearch = faSearch
   thumbsUp = faThumbsUp
   thumbsDown = faThumbsDown

  allUsers: UserDetails[] = []
  showUsers: UserDetails[] = []

  constructor(private router: Router, private userService: UserService, private eventService: EventService) {
    this.searchForm = new FormGroup({
      search: new FormControl()
    })
  }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(users => {
      this.allUsers = users;
      this.showUsers = users;
    })

    this.searchForm.get("search").valueChanges.subscribe(input => {
      this.showUsers = this.allUsers.filter((e, i, a) => {
        return new RegExp((input ?? "").replace(".", "\\."), "ig").exec(e.fullName) != null
      })
    })
  }

  GetUserDetails(userDetails: UserDetails) {
    console.log("Source" + userDetails.fullName);
    this.eventService.emit<UserDetails>(userDetails);
    this.router.navigateByUrl(`qna/users/view/${userDetails.id}`);

  }
}
