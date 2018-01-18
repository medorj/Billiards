import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BilliardsService } from './billiards.service';
import { IMatch } from './match.model';
import { Match } from './match.class';

@Component({
    templateUrl: './match-details.component.html',
    styleUrls: ['./match-details.component.css']
})

export class MatchDetailsComponent implements OnInit{
    match: IMatch;
    matchClass: Match;
    innings: number = 0;
    defensiveShots: number = 0;
    sortDirection: string = "DESC";

    constructor(private billiardsService: BilliardsService,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        let sort: string = localStorage.getItem('matchSort');
        if (sort) {
            this.sortDirection = sort;
        } else {
            localStorage.setItem('matchSort', this.sortDirection);
        }
        this.refreshGames();
    }

    refreshGames() {
        let id: number = +this.route.snapshot.params['id'];

        this.billiardsService.getMatch(id, this.sortDirection).subscribe(
            data => {
                this.match = data;
                this.matchClass = new Match(this.match);
                this.aggregateGames();
            }
        );
    }

    aggregateGames() {
        this.innings = 0;
        this.match.Games.forEach(g => {
            this.innings += g.Innings;
        });
    }
    totalGames(): number {
        return this.match.Games.filter(g => g.WinnerUserId > 0).length;
    }
    totalInnings(): number {
        let innings: number = 0;
        this.match.Games.forEach(g => {
            innings += g.Innings;
        });
        return innings;
    }
    hasUnfinishedGames(): boolean {
        if (!this.match || !this.match.Games) {
            return false;
        }
        let gamesWithWins = this.match.Games.filter(g => g.WinnerUserId > 0);
        if (gamesWithWins.length < this.match.GameCount) {
            return true;
        }
        return false;
    }
    user1Text(): string {
        return this.match.User1Name + ": " + this.match.User1Wins;
    }
    user2Text(): string {
        return this.match.User2Name + ": " + this.match.User2Wins;
    }
    user1Percentage() : string {
        if (this.match && this.match.GameCount > 0) {
            return ((this.match.User1Wins / this.totalGames()) * 100) + '%';
        }
        return "0%";
    }
    user2Percentage() {
        if (this.match && this.match.GameCount > 0) {
            return ((this.match.User2Wins / this.totalGames()) * 100) + '%';
        }
        return "0%";
    }

    addGame(){
        let matchId : number = +this.route.snapshot.params['id'];
        this.billiardsService.addGame({ MatchId: matchId}).subscribe(
            data => {
                this.router.navigate(['/matches', matchId, 'game', data.GameId]);
                this.billiardsService.getMatch(matchId, this.sortDirection).subscribe(
                    data => {
                        this.match = data
                    }
                );
            }
        );
    }

    deleteGame() {
        this.billiardsService.deleteMatch(this.match).subscribe(
            data => {
                this.router.navigate(["matches"]);
            });
    }

    toggleSort() {
        if (this.sortDirection == "ASC") {
            this.sortDirection = "DESC";
        } else {
            this.sortDirection = "ASC";
        }
        localStorage.setItem('matchSort', this.sortDirection);
        this.refreshGames();
    }
}