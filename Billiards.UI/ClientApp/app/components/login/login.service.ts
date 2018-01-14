import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ILogin } from './login.model';
import { Observable } from 'rxjs/Observable';
import { ConfigurationService } from '../common/configuration.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' })
}

@Injectable()
export class LoginService {
    currentUser: ILogin;
    baseUrl: string;

    constructor(private http: HttpClient, private config: ConfigurationService) {
        this.baseUrl = config.baseUrl;
    }

    loginUser(userName: string, password: string): Observable<any> {
        let login = {
            UserName: userName,
            Password: password
        };

        return this.http.post(this.baseUrl + 'Auth/Login', login, httpOptions).map(
            (data) => {
                if (data) {
                    return data;
                } else {
                    return {};
                }
            })
            .do(
            (user: any) => {
                if (!!user.UserName) {
                    sessionStorage["loginId"] = user.LoginId;
                    this.currentUser = <ILogin>user;
                }
            })
            .catch(error => {
                sessionStorage["loginId"] = false;
                return Observable.of(false);
            });
    }

    checkIsAuthenticated () {
        if (sessionStorage["loginId"]) {
            let login = {
                LoginId: +sessionStorage["loginId"]
            }
            this.http.post(this.baseUrl + 'Auth/GetLogin', login, httpOptions).subscribe(
                (login : ILogin) => {
                    this.currentUser = login;
                }
            )
            return true;
        }
        return false;
    }

    getLogin() {
        let login = {
            LoginId: +sessionStorage["loginId"]
        }
        return this.http.post(this.baseUrl + 'Auth/GetLogin', login, httpOptions);
    }

    register(loginValues : ILogin): Observable<any> {
        return this.http.post(this.baseUrl + 'Auth/Register', loginValues, httpOptions);
    }

    editRegistration(loginValues: ILogin): Observable<any> {
        return this.http.post(this.baseUrl + 'Auth/EditRegistration', loginValues, httpOptions);
    }

    isAuthed(): Observable<boolean> {
        return Observable.of(!!this.currentUser);
    }

    isAuthenticated() {
        return !!this.currentUser;
    }

    logout() {
        sessionStorage.removeItem("loginId");
        this.currentUser = null;
    }
}