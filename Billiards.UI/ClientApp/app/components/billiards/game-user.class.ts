import { IGameUser } from './match.model';

export class GameUser{
    GameUserId: number;
    GameId: number;
    UserId: number;
    UserName: string;
    DefensiveShots: number = 0;
    Timeouts: number = 0;

    constructor(gameUser: IGameUser) {
        this.GameUserId = gameUser.GameUserId;
        this.GameId = gameUser.GameId;
        this.UserId = gameUser.UserId;
        this.UserName = gameUser.UserName;
        this.DefensiveShots = gameUser.DefensiveShots;
        this.Timeouts = gameUser.Timeouts;
    }    

    incrementDefensive() {
        this.DefensiveShots = this.DefensiveShots + 1;
    }

    decrementDefensive() {
        if (this.DefensiveShots > 0) {
            this.DefensiveShots = this.DefensiveShots - 1;
        }
    }

    incrementTimeout() {
        this.Timeouts = this.Timeouts + 1;
    }

    decrementTimeout() {
        if (this.Timeouts > 0) {
            this.Timeouts = this.Timeouts - 1;
        }
    }
}