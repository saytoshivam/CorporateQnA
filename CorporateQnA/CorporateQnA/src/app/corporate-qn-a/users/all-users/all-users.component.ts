import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserDetails } from 'src/app/shared/models';
import { EventService, UserService } from 'src/app/shared/services';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styles: []
})
export class AllUsersComponent implements OnInit {
  searchForm: FormGroup;
  // faSearch = faSearch
  // thumbsUp = faThumbsUp
  // thumbsDown = faThumbsDown

  allUsers: UserDetails[] = []
  showUsers: UserDetails[] = []

  constructor(private userService: UserService, private eventService: EventService) {
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

  GetUserDetails(userDetails) {
    this.eventService.emit<UserDetails>(userDetails);
  }
}
