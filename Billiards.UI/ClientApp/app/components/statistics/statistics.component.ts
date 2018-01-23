import { Component } from '@angular/core';
import { IUser } from '../user/user.model';
import { UserService } from '../user/user.service';
import { BilliardsService } from '../billiards/billiards.service';

@Component({
    templateUrl: './statistics.component.html'
})

export class StatisticsComponent {
    player1Users: IUser[] = [];
    player1: number = 0;
    player2Users: IUser[] = [];
    player2: number = 0;
    compareData: any;
    enable: boolean = false;

    constructor(private userService: UserService, private billiardsService: BilliardsService) { }

    ngOnInit() {
        this.userService.getUsers().subscribe(data => {
            this.player1Users = data;
            this.player2Users = data;
        });
    }

    enableCompare(p1: number, p2: number) {
        this.enable = p1 > 0 && p2 > 0;
    }

    compare(p1: number, p2: number) {
        this.billiardsService.getHeadToHead(p1, p2).subscribe(data => {
            this.compareData = data;
        });
    }
}