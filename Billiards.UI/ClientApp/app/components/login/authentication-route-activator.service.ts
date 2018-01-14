import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { LoginService } from './login.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthenticationRouteActivator implements CanActivate {
    constructor(private loginService: LoginService,
        private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot) {
        return this.loginService.checkIsAuthenticated();
    }
}