import { Component, OnInit } from '@angular/core';
import { ILogin } from '../login/login.model';
import { IUser } from './user.model';
import { UserService } from './user.service';

@Component({
    templateUrl: './link-user.component.html'
})

export class LinkUserComponent implements OnInit {
    unlinkedLogins: ILogin[];
    unlinkedLogin: ILogin = null;
    unlinkedUsers: IUser[];
    unlinkedUser: IUser = null;
    showSuccess: boolean = false;
    showError: boolean = false;
    message: string;

    constructor(private userService: UserService) {

    }

    ngOnInit() {
        this.refreshData();
    }

    refreshData() {
        this.userService.getUnlinkedAccounts().subscribe(data => {
            this.unlinkedUser = null;
            this.unlinkedLogin = null;
            this.unlinkedLogins = data.logins;
            this.unlinkedUsers = data.users;
        });
    }

    saveLinkedAccount(account: any) {
        this.userService.saveLinkedAccount(account).subscribe(data => {
            this.refreshData();
            this.showSuccess = true;
            this.showError = false;
            this.message = "The account sync was successful";
        }, error => {
            this.showSuccess = true;
            this.showError = false;
            this.message = "There was an error syncing the accounts";
        });
    }
}