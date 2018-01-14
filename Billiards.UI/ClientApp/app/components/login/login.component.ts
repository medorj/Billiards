import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

@Component({
    templateUrl: './login.component.html',
    styles: [`
        em {float: right; color: #E05C65; padding-left: 10px; }
    `]
})

export class LoginComponent {
    userName: string;
    password: string;
    invalidLogin: boolean = false;
    isLoggingIn: boolean = false;

    constructor(private loginService: LoginService, private router: Router) {

    }

    login(formValues: any) {
        this.isLoggingIn = true;
        this.loginService.loginUser(formValues.userName, formValues.password).subscribe(
            data => {
                if (!data) {
                    this.isLoggingIn = false;
                    this.invalidLogin = true;
                } else {
                    this.router.navigate(['home']);
                }
            }
        );
    }

    cancel() {
        this.router.navigate(['home']);
    }
}