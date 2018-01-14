import { Component } from '@angular/core';
import { UserService } from './user.service';
import { IUser } from './user.model';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
    templateUrl: './user-details.component.html'
})

export class UserDetailsComponent{
    user : IUser;

    constructor(private userService: UserService,
        private route: ActivatedRoute,
        private router: Router){

    }

    ngOnInit(){
        this.userService.getUser(+this.route.snapshot.params['id']).subscribe(
            data => {
                this.user = data
            }
        );
    }

    deleteUser(){
        this.userService.deleteUser(this.user).subscribe(
            data => this.router.navigate(['/user'])
        )
    }
}