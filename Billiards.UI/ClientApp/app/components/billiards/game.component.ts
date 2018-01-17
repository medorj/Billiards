import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BilliardsService } from './billiards.service';
import { IGame, IGameUser } from './match.model';
import { IUser } from '../user/user.model';
import { GameUser } from './game-user.class';

@Component({
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit{
    game : IGame;
    isEdit: boolean = false;
    selectedWinner: IUser;
    opponents: IUser[];
    pageLoaded: boolean = false;
    isSaving: boolean = false;
    gameUsers: GameUser[];

    constructor(private billiardsService : BilliardsService, private route: ActivatedRoute, private router: Router){ }

    decrementInnings() {
        if (this.game.Innings > 0) {
            this.game.Innings = this.game.Innings - 1;
        }
    }

    incrementInnings() {
        this.game.Innings = this.game.Innings + 1;
    }

    refreshGame() {
        let id: number = +this.route.snapshot.params["gameId"];
        this.billiardsService.getGame(id).subscribe(
            data => {
                this.game = data;
                let array = new Array();
                let participants: IUser[] = data.Participants;
                participants.forEach(p => {
                    if (this.game.WinnerUserId == p.UserId) {
                        p.IsSelected = true;
                        this.selectedWinner = p;
                    } else {
                        p.IsSelected = false;
                    }
                });
                this.opponents = participants;

                let userArray = new Array();
                this.game.GameUsers.forEach((gu : IGameUser) => {
                    userArray.push(new GameUser(gu));
                });
                this.gameUsers = userArray;
                this.pageLoaded = true;
            }
        );
    }

    ngOnInit(){
        this.refreshGame();
    }

    selectWinner(user: IUser) {
        this.selectedWinner = user;
        this.game.WinType = 1;
        this.opponents.forEach(o => {
            if (o.UserId === user.UserId) {
                o.IsSelected = true
            } else {
                o.IsSelected = false;
            }
        });
    }

    private serializeGameUsers() : IGameUser[] {
        let array : IGameUser[] = new Array();
        this.gameUsers.forEach((g : GameUser)  => {
            array.push({
                GameUserId: g.GameUserId,
                DefensiveShots: g.DefensiveShots,
                Timeouts: g.Timeouts
            });
        })
        return array;
    }

    saveGame(formValues: IGame) {
        this.isSaving = true;
        let gameToSave: IGame = formValues;
        if (this.selectedWinner) {
            gameToSave.WinnerUserId = this.selectedWinner.UserId;
        }
        gameToSave.GameUsers = this.serializeGameUsers();
        this.billiardsService.saveGame(gameToSave).subscribe(
            data => {
                this.game = data;
                this.isEdit = false;
                this.isSaving = false;
            }
        );
    }

    cancel() {
        this.isEdit = false;
        this.refreshGame();
    }

    toggleEdit(){
        this.isEdit = !this.isEdit;
    }

    setEdit(value: boolean) {
        this.isEdit = value;
    }
}