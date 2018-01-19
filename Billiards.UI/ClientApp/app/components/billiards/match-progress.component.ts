import { Component, Input } from '@angular/core';
import { Match } from './match.class';
import { ShortNamePipe } from './name.pipe';

@Component({
    selector: 'match-progress',
    template: `
        <div class="progress">
            <div class="progress-bar progress-bar-success" [style.width]="match.user1Percentage()">
                {{match.match.User1Name | shortname}} : {{match.match.User1Wins}}
            </div>
            <div class="progress-bar progress-bar-danger" [style.width]="match.user2Percentage()">
                {{match.match.User2Name | shortname}} : {{match.match.User2Wins}}
            </div>
        </div>
    `,
    styles: [
        `
            .progress{
                margin-bottom: 0;
            }
        `
    ]
})

export class MatchProgressComponent {
    @Input() match: Match;
}