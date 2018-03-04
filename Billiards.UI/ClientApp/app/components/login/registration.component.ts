import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from './login.service';
import { ILogin } from './login.model';

@Component({
    templateUrl: './registration.component.html',
    styles: [`
        em {float: right; color: #E05C65; padding-left: 10px; }
    `]
})

export class RegistrationComponent {
    LoginId: number;
    FirstName: string;
    LastName: string;
    UserName: string;
    Password: string;
    errorMessage: string;
    isEdit: boolean = false;
    isSaving: boolean = false;
    hasError: boolean = false;

    constructor(private loginService: LoginService, private router: Router, private route: ActivatedRoute) {
        
    }

    ngOnInit() {
        this.route.url.forEach(url => {
            url.forEach(path => {
                if (path.path == "edit") {
                    this.isEdit = true;
                }
            });
        })
        if (this.isEdit) {
            this.loginService.getLogin().subscribe((data : ILogin) => {
                if (data) {
                    this.FirstName = data.FirstName;
                    this.LastName = data.LastName;
                    this.UserName = data.UserName;
                    this.Password = data.Password;
                    this.LoginId = data.LoginId;
                }
            });
        }
    }

    saveRegistration(formValues: ILogin) {
        this.isSaving = true;
        this.loginService.register(formValues).subscribe(
            data => {
                this.router.navigate(['/home']);
            },
            error => {
                this.errorMessage = "Cannot save registration information. You may have entered duplicate information.";
                this.hasError = true;
                this.isSaving = false;
            }
        );
    }

    editRegistration(formValues: ILogin) {
        this.loginService.editRegistration(formValues).subscribe(data => {
            this.router.navigate(['/home']);
        });
    }

    cancel() {
        this.router.navigate(['/login']);
    }
}