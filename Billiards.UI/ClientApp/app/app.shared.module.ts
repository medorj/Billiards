import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from "./components/common/nav-menu.component";
import { Error404Component } from './components/errors/404.component';
import {
    // components
    UserComponent,
    CreateUserComponent,
    UserDetailsComponent,
    MatchesComponent,
    MatchDetailsComponent,
    CreateMatchComponent,
    GameComponent,
    LoginComponent,
    LoginService,
    MatchProgressComponent,
    RegistrationComponent,
    // services
    BilliardsService,
    UserService,
    ConfigurationService,
    // route guards
    AuthenticationRouteActivator,
    // pipes
    ShortNamePipe
} from './index';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        UserComponent,
        CreateUserComponent,
        UserDetailsComponent,
        MatchesComponent,
        MatchDetailsComponent,
        CreateMatchComponent,
        GameComponent,
        LoginComponent,
        Error404Component,
        MatchProgressComponent,
        ShortNamePipe,
        RegistrationComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'user', component: UserComponent },
            { path: 'user/new', component: CreateUserComponent },
            { path: 'user/:id', component: UserDetailsComponent },
            { path: 'matches', component: MatchesComponent, canActivate: [AuthenticationRouteActivator]},
            { path: 'matches/new', component: CreateMatchComponent, canActivate: [AuthenticationRouteActivator] },
            { path: 'matches/:id', component: MatchDetailsComponent, canActivate: [AuthenticationRouteActivator]  },
            { path: 'matches/:matchId/game/:gameId', component: GameComponent, canActivate: [AuthenticationRouteActivator] },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegistrationComponent },
            { path: 'profile/edit', component: RegistrationComponent},
            { path: '404', component: Error404Component },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [UserService, BilliardsService, LoginService, ConfigurationService, AuthenticationRouteActivator]
})
export class AppModuleShared {
}
