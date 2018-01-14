import { Component } from '@angular/core';
import { LoginService } from '../login/login.service';

@Component({
    selector: 'app-navmenu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent {
    showNav: boolean = false;

    loginService: LoginService;
    constructor(private _loginService: LoginService) {
        this.loginService = _loginService;
    }

    toggleNav() {
        this.showNav = !this.showNav;
    }

    hideNav() {
        this.showNav = false;
    }

    logout() {
        this.loginService.logout();
    }
}