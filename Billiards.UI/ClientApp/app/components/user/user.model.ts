export interface IUser{
    UserId: number;
    FirstName: string;
    LastName: string;
    UserName: string;
    Handicap: number;
    IsSelected?: boolean;
    GamesPlayed?: number;
    GamesWon?: number;
    WinPercentage?: number;
}