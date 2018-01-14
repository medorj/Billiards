import { IUser } from '../user/user.model';

export interface IMatch{
    MatchId: number;
    Title: string;
    Date: Date;
    Games?: IGame[];
    GameCount: number;
    User1Name: string;
    User2Name: string;
    User1Wins: number;
    User2Wins: number;
}

export interface IGame{
    GameId: number;
    MatchId: number;
    Match: IMatch;
    Number: number;
    WinnerUserId: number;
    WinnerName: string;
    Innings: number;
    GameUsers: IGameUser[];
}

export interface IGameUser {
    GameUserId: number;
    GameId?: number;
    UserId?: number;
    UserName?: string;
    DefensiveShots?: number;
    Timeouts?: number;
}