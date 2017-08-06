import { Component, Inject } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Http } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Samples } from '../shared';

@Component({
    selector: 'samples-by-name',
    templateUrl: './samples-by-name.component.html'
})

export class SampleByNameComponent {
    public samples: Samples[];
    public searchInput;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {}

    public searchSamples() {
        const postJson = {
            Name: this.searchInput
        };

        this.http.post(this.originUrl + '/api/Samples/Search', postJson).subscribe(result => {
            this.samples = result.json() as Samples[];
        });
    }

}