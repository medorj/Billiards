import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'shortname' })
export class ShortNamePipe {
    transform(value: string) {
        if (value.length > 0) {
            let name: string;
            let names = value.split(' ');
            let firstInitial: string = names[0].charAt(0).toLocaleUpperCase() + '. ';
            name = firstInitial;

            let remainingNames: string;
            names = names.slice(1);
            names.forEach(n => {
                name += " " + n;
            })
            return name;
        }
    }
}