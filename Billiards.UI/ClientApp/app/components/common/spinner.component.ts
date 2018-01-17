import { Component, Input } from '@angular/core';

@Component({
    selector: 'spinner',
    template: `
        <div style="margin-top: 50px; text-align: center;">
            <img height="100" src="https://www.voki.com/images/ajax-loader.gif" />
            <div style="font-size: 18px;">{{loadingText}}</div>
        </div>
    `
})

export class SpinnerComponent {
    @Input() loadingText: string = "Loading...";
}