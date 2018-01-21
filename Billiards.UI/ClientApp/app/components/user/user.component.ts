import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { IUser } from './user.model';

@Component({
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit{
    users: IUser[];
    pageLoaded: boolean = false;

    constructor(private userService: UserService){

    }

    ngOnInit(){
        this.userService.getUsers().subscribe(
            data => {
                this.users = data;
                this.pageLoaded = true;
            },
            error => {
                console.log('User API: Error Getting User Data');
            }
        )
    }

}