import { Component, Inject } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Http } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Samples, Status, User } from '../shared'

@Component({
    selector: 'add-sample',
    templateUrl: './add-samples.component.html'
})

export class AddSampleComponent {
    public sample; 
    public statuses: Status[];
    public users: User[];
    public success: boolean = false;
    public fail: boolean = false;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {
        this.sample = { barCode: null, createdAt: null, createdBy: null, statusId: null };

        //Get Users for dropdown
        http.get(originUrl + '/api/Users').subscribe(result => {
            this.users = result.json() as User[];
        });

        // Get Statuses for dropdown
        http.get(originUrl + '/api/Statuses').subscribe(result => {
            this.statuses = result.json() as Status[];
        });
    }

    public addSample() {
        this.success = false;
        this.fail = false;

        this.http.post(this.originUrl + '/api/Samples', this.sample).subscribe(
            (response) => {
                this.success = true;
            },
            (err) => {
                this.fail = true;
            }
        );
    }

    public validForm() {
        return (this.sample.barcode != null
            && this.sample.createdAt != null
            && this.sample.createdBy != null
            && this.sample.statusId != null);
    }
}