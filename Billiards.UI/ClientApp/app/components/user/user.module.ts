import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared.module';
import {
    CreateUserComponent,
    UserComponent,
    UserService,
    UserDetailsComponent,
    LinkUserComponent
} from './index';

@NgModule({
    declarations: [
        CreateUserComponent,
        UserComponent,
        UserDetailsComponent,
        LinkUserComponent
    ], 
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'user', component: UserComponent },
            { path: 'user/link', component: LinkUserComponent },
            { path: 'user/new', component: CreateUserComponent },
            { path: 'user/profile', component: UserDetailsComponent },
            { path: 'user/:id', component: UserDetailsComponent }
        ])
    ],
    providers: [UserService]
})

export class UserModule {

}