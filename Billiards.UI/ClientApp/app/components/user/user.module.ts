import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared.module';
import {
    CreateUserComponent,
    UserComponent,
    UserService,
    UserDetailsComponent
} from './index';

@NgModule({
    declarations: [
        CreateUserComponent,
        UserComponent,
        UserDetailsComponent
    ], 
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'user', component: UserComponent },
            { path: 'user/new', component: CreateUserComponent },
            { path: 'user/:id', component: UserDetailsComponent },
        ])
    ],
    providers: [UserService]
})

export class UserModule {

}