import { Component } from '@angular/core';
import { UserService } from './user.service';
import { Router } from '@angular/router';

@Component({
    templateUrl: './create-user.component.html'
})

export class CreateUserComponent{
    FirstName: string;
    LastName: string;

    constructor(private userService: UserService, private router: Router){

    }

    saveUser(user: any){
        this.userService.addUser(user).subscribe(
            data => {
                this.router.navigate(['/user']) 
            }
        );
    }
}