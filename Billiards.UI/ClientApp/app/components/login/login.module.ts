import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { RouterModule } from '@angular/router';
import {
    LoginComponent,
    LoginService,
    RegistrationComponent
} from './index'

@NgModule({
    declarations: [
        LoginComponent,
        RegistrationComponent
    ],
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegistrationComponent },
            { path: 'profile/edit', component: RegistrationComponent },
        ])
    ],
    providers: [ LoginService ]
})

export class LoginModule {

}