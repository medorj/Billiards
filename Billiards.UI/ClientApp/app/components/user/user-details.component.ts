import { Component } from '@angular/core';
import { UserService } from './user.service';
import { IUser } from './user.model';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
    templateUrl: './user-details.component.html',
    styles: [`
        .data-value {
            font-size: 24px;
        }
    `]
})

export class UserDetailsComponent{
    user: IUser;
    isLoading: boolean = false;
    hasError: boolean = false;
    errorMessage: string = "Unable to access your user profile. Your login may not be linked to your user profile.";

    constructor(private userService: UserService,
        private route: ActivatedRoute,
        private router: Router){

    }

    ngOnInit() {
        this.isLoading = true;
        let id = +this.route.snapshot.params['id'];
        if (!id) {
            id = sessionStorage["userId"];
        }
        this.userService.getUser(id).subscribe(
            data => {
                this.user = data;
                this.isLoading = false;
            },
            error => {
                this.hasError = true;
                this.isLoading = false;
            }
        );
    }

    deleteUser(){
        this.userService.deleteUser(this.user).subscribe(
            data => this.router.navigate(['/user'])
        )
    }
}