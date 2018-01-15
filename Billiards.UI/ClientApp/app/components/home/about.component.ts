import { Component } from '@angular/core';

@Component({
    template: `
        <h1>About</h1>
        <div class="row">
            <div class="col-xs-4">
                <h2>Record Your Matches</h2>
                <p>Record the matches that you play.</p>
            </div>
            <div class="col-xs-4">
                <h2>View Your History</h2>
                <p>Go back and find out how you did in previous matches.</p>
            </div>
            <div class="col-xs-4">
                <h2>Manage Your Users</h2>
                <p>Manage the users that can participate in matches.</p>
            </div>
        </div>
    `
})

export class AboutComponent {

}