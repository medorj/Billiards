import { IUser } from '../user/user.model';

export interface IMatch{
    MatchId: number;
    Title: string;
    Date: Date;
    Games?: IGame[];
    GameCount: number;
    User1Name: string;
    User1Wins: number;
    User1Points: number;
    User1WinPercentage: number;
    User1WinsRemaining: number;
    User2Name: string;
    User2Wins: number;
    User2Points: number;
    User2WinsRemaining: number;
    MatchTypeId: number;
    User2WinPercentage: number;
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
    WinType: number;
    Badge: number;
}

export interface IGameUser {
    GameUserId: number;
    GameId?: number;
    UserId?: number;
    UserName?: string;
    DefensiveShots?: number;
    Timeouts?: number;
}