import { Component, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Http } from '@angular/http';
import { Samples } from '../shared';

@Component({
    selector: 'samples',
    templateUrl: './samples.component.html'
})

export class SampleComponent {
    public samples: Samples[];
    public filter: Samples = new Samples();

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Samples').subscribe(result => {
            this.samples = result.json() as Samples[];
        });
    }
}