import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SpinnerComponent } from './index';

@NgModule({
    declarations: [
        SpinnerComponent
    ],
    imports: [
    ],
    providers: [
    ],
    exports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        SpinnerComponent
    ]
})

export class SharedModule {

}