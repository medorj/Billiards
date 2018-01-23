import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { IMatch, IGame } from './match.model';
import { HttpResponse } from '@angular/common/http/src/response';
import { ConfigurationService } from '../common/configuration.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8'})
}

@Injectable()
export class BilliardsService{
    baseUrl: string;
    constructor(private http: HttpClient, private config: ConfigurationService){
        this.baseUrl = config.baseUrl;
    }

    getMatches() : Observable<IMatch[]> {
        return this.http.get(this.baseUrl + 'Billiards/GetMatches').catch(this.handleError)
    }

    getMatch(id: number, orderBy: string){
        return this.http.get(this.baseUrl + 'Billiards/GetMatch?id=' + id + "&orderBy=" + orderBy).catch(this.handleError);
    }

    addMatch(match: IMatch) : Observable<IMatch>{
        return this.http.post(this.baseUrl + 'Billiards/SaveMatch', match, httpOptions).catch(this.handleError);
    }

    deleteMatch(match: IMatch): Observable<IMatch> {
        return this.http.post(this.baseUrl + 'Billiards/DeleteMatch', match, httpOptions).catch(this.handleError);
    }

    addGame(game: any){
        let body = JSON.stringify(game);
        return this.http.post(this.baseUrl + 'Billiards/AddGame', body, httpOptions).catch(this.handleError);
    }

    getGame(id: number){
        return this.http.get(this.baseUrl + 'Billiards/GetGame?id=' + id).catch(this.handleError);
    }

    saveGame(game: any){
        let body = JSON.stringify(game);
        return this.http.post(this.baseUrl + 'Billiards/SaveGame', body, httpOptions).catch(this.handleError);
    }

    deleteGame(game: any) {
        return this.http.post(this.baseUrl + 'Billiards/DeleteGame', game, httpOptions).catch(this.handleError);
    }

    getHeadToHead(player1: number, player2: number) {
        return this.http.get(this.baseUrl + 'Billiards/GetHeadToHead?player1=' + player1 + "&player2=" + player2);
    }

    private handleError(error:any){
        console.log('HTTP ERROR: ' + error);
        return Observable.throw(error.statusText);
    }
}