import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BilliardsService } from './billiards.service';
import { IUser } from '../user/user.model';
import { UserService } from '../user/user.service';
import { Observable } from 'rxjs/Observable';

@Component({
    templateUrl: './create-match.component.html'
})

export class CreateMatchComponent implements OnInit{
    Date: string = "01/08/2018";
    users: IUser[] = [];
    User1Id: number = 0;
    User2Id: number = 0;

    constructor(private billiardsService: BilliardsService,
        private userService: UserService,
        private router: Router){
    }

    ngOnInit(){
        this.userService.getUsers().subscribe(
            data => {
                this.users = data
            }, 
            error => {
                console.log('ERROR: Error getting users')
            }
        );
    }

    saveMatch(formValues: any){
        this.billiardsService.addMatch(formValues).subscribe(
            data => {
                this.router.navigate(['/matches', data.MatchId])
            }
        );
    }
}