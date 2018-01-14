import { Component } from '@angular/core';
import { LoginService } from '../login/login.service';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent {
    isLoggedIn: boolean;

    constructor(private loginService: LoginService) {
    }

    ngOnInit() {
        this.isLoggedIn = this.loginService.checkIsAuthenticated();
    }
}