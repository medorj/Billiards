import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared.module';
import {
    MatchesComponent,
    MatchDetailsComponent,
    CreateMatchComponent,
    GameComponent,
    MatchProgressComponent,
    BilliardsService,
    ShortNamePipe,
    GameDetailsComponent
} from './index';
import { AuthenticationRouteActivator} from '../login/authentication-route-activator.service'

@NgModule({
    declarations: [
        MatchesComponent,
        MatchDetailsComponent,
        CreateMatchComponent,
        GameComponent,
        MatchProgressComponent,
        ShortNamePipe,
        GameDetailsComponent
    ],
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'matches', component: MatchesComponent, canActivate: [AuthenticationRouteActivator] },
            { path: 'matches/new', component: CreateMatchComponent, canActivate: [AuthenticationRouteActivator] },
            { path: 'matches/:id', component: MatchDetailsComponent, canActivate: [AuthenticationRouteActivator] },
            { path: 'matches/:matchId/game/:gameId', component: GameComponent, canActivate: [AuthenticationRouteActivator] },
        ])
    ],
    providers: [BilliardsService]
})

export class BilliardsModule {

}