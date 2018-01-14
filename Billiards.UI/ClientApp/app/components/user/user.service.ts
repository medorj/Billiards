import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { IUser } from './user.model';
import { ConfigurationService } from '../common/configuration.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8'})
}

@Injectable()
export class UserService{
    currentUser: IUser;
    baseUrl: string;

    constructor(private http: HttpClient, private config: ConfigurationService){
        this.baseUrl = config.baseUrl;
    }

    getUsers() : Observable<IUser[]> {
        return this.http.get(this.baseUrl + 'Billiards/GetUsers').catch(this.handleError);
    }

    getUser(id: number){
        return this.http.get(this.baseUrl + 'Billiards/GetUser?id=' + id).catch(this.handleError);
    }

    addUser(user: any){
        let body = JSON.stringify(user);
        return this.http.post(this.baseUrl + 'Billiards/SaveUser', body, httpOptions).catch(this.handleError);
    }

    deleteUser(user: any){
        let body = JSON.stringify(user);
        return this.http.post(this.baseUrl + 'Billiards/DeleteUser', body, httpOptions).catch(this.handleError);
    }

    private handleError(error:any){
        console.log('HTTP ERROR: ' + error);
        return Observable.throw(error.statusText);
    }
}