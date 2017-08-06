import { Component, Inject } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Http } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Samples, Status } from '../shared';

@Component({
    selector: 'samples-by-status',
    templateUrl: './samples-by-status.component.html'
})

export class SampleByStatusComponent {
    public samples: Samples[];
    public statuses: Status[];
    public selectedStatus;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {
        http.get(originUrl + '/api/Statuses').subscribe(result => {
            this.statuses = result.json() as Status[];
        });
    }

    public searchSamples() {
        const postJson = {
            StatusId: this.selectedStatus
        };

        this.http.post(this.originUrl + '/api/Samples/Search', postJson).subscribe(result => {
            this.samples = result.json() as Samples[];
        });
    }
}