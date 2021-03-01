import { Component, OnInit } from '@angular/core';
import { UserDetails } from '../../shared/models';
import { UserService } from '../../shared/services';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styles: [
  ]
})
export class UserComponent implements OnInit {

  userDetailsList: UserDetails[];
  constructor(private userService: UserService) { }

  ngOnInit(): void {

    this.userService.getAllUsers().subscribe(res => {
      this.userDetailsList = <UserDetails[]>res;
      console.log(this.userDetailsList);
    })
  }
}