import { Injectable } from '@angular/core';

@Injectable()
export class ConfigurationService {
    baseUrl: string = 'http://localhost:49971/api/';
    //baseUrl: string = 'http://billiardsservice.azurewebsites.net/api/';
}