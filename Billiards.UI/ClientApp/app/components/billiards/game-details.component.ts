import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { IGame, IGameUser } from './match.model';
import { GameUser } from './game-user.class';
import { BilliardsService } from './billiards.service';

@Component({
    selector: 'game-details',
    styles: [`
        .small-header{
            font-weight: bold;
            margin: 5px 0;
        }
        .header-minor {
            font-size: 18px;
        }
    `],
    template: `
    <h3>{{game?.Match.Title}}</h3>
        <div class="row">
            <div class="col-xs-4">
                <div class="small-header">DATE</div>
                <div class="header-minor">{{game?.Match.Date | date: 'MM/dd/yyyy'}}</div>
            </div>
            <div class="col-xs-4">
                <div class="small-header">WINNER</div>
                <div class="header-minor">{{game?.WinnerName}}</div>
            </div>
            <div class="col-xs-4">
                <div class="small-header">INNINGS</div>
                <div class="header-minor">{{game?.Innings}}</div>
            </div>
        </div>
        <h4>Game Statistics</h4>
        <table class="table">
            <thead>
                <tr>
                    <th>Player</th>
                    <th>Defensive Shots</th>
                    <th>Time Outs</th>
                </tr>
            </thead>
            <tbody>
                <tr *ngFor="let gameUser of gameUsers">
                    <td>{{gameUser.UserName}}</td>
                    <td>{{gameUser.DefensiveShots}}</td>
                    <td>{{gameUser.Timeouts}}</td>
                </tr>
            </tbody>
        </table>
        <hr />
        <button class="btn btn-primary" (click)="toggleEdit()">Edit Game</button>
        <button class="btn btn-default" (click)="goToMatch()">Back to Match</button>
        <button class="btn btn-danger" (click)="deleteGame()">Delete Game</button>
    `
})

export class GameDetailsComponent {
    @Input() game: IGame;
    @Input() gameUsers: GameUser[];
    @Output() refreshGame = new EventEmitter();
    @Output() editChanged = new EventEmitter();

    constructor(private billiardsService: BilliardsService, private router: Router) {

    }

    cancel() {
        this.editChanged.emit(false);
        this.refreshGame.emit(true);
    }

    toggleEdit() {
        this.editChanged.emit(true);
    }

    deleteGame() {
        this.billiardsService.deleteGame(this.game).subscribe(data => {
            this.router.navigate(['matches', this.game.MatchId]);
        });
    }

    goToMatch() {
        this.router.navigate(['matches', this.game.Match.MatchId]);
    }
}