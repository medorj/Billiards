import { IGame, IMatch } from './match.model';

export class Match {
    match: IMatch;
    gameCount: number;
    games: IGame[];
    user1Name: string;
    user1Wins: number;
    user2Name: string;
    user2Wins: number;
    user1WinPercentage: number;
    user2WinPercentage: number;

    constructor(match: IMatch) {
        this.match = match;
        this.gameCount = match.GameCount;
        this.games = match.Games;
        this.user1Name = match.User1Name;
        this.user1Wins = match.User1Wins;
        this.user2Name = match.User2Name;
        this.user2Wins = match.User2Wins;
    }

    hasUnfinishedGames(): boolean {
        let gamesWithWins = this.games.filter(g => g.WinnerUserId > 0);
        if (gamesWithWins.length < this.gameCount) {
            return true;
        }
        return false;
    }

    totalGames(): number {
        return this.games.filter(g => g.WinnerUserId > 0).length;
    }

    user1Text(): string {
        return this.user1Name + ": " + this.user1Wins;
    }

    user2Text(): string {
        return this.user2Name + ": " + this.user2Wins;
    }

    user1Percentage(): string {
        if (this.gameCount > 0) {
            return ((this.user1Wins / this.totalGames()) * 100) + '%';
        }
        return "0%";
    }

    user2Percentage() {
        if (this.gameCount > 0) {
            return ((this.user2Wins / this.totalGames()) * 100) + '%';
        }
        return "0%";
    }
}