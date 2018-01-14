import { Component, OnInit } from '@angular/core';
import { BilliardsService } from './billiards.service';
import { IMatch } from './match.model';
import { Match } from './match.class';

@Component({
    templateUrl: './matches.component.html',
    styleUrls: ['./matches.component.css']
})

export class MatchesComponent implements OnInit{
    matches: IMatch[];
    matchClasses: Match[];
    viewAsList: boolean = false;
    pageLoaded: boolean = false;
    
    constructor(private billiardsService: BilliardsService){
    }

    ngOnInit(){
        this.billiardsService.getMatches().subscribe(
            data => {
                this.matches = data;
                this.pageLoaded = true;
                this.matchClasses = this.matches.map(c => {
                    return new Match(c);
                })
            }
        );
    }

    toggleList(){
        this.viewAsList = !this.viewAsList;
    }
}