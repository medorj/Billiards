import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { BilliardsService } from './billiards.service';

@Injectable()
export class BilliardsRouteActivator implements CanActivate{
    constructor(private billiardsService: BilliardsService,
        private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot) {
        let matchExists = true;
        this.billiardsService.getMatch(+route.params['id'], "ASC").subscribe(
            data => {
                matchExists = true;
            },
            err => {
                this.router.navigate(['/404']);
                
            });
        return matchExists;
    }
}