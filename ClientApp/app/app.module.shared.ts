import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { SampleComponent } from './components/samples/samples.component';
import { SampleByStatusComponent } from './components/samples/samples-by-status.component';
import { SampleByNameComponent } from './components/samples/samples-by-name.component';
import { AddSampleComponent } from './components/samples/add-samples.component';
import { SampleFilterPipe } from './components/shared/samples-filter.pipe';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        SampleComponent,
        SampleByStatusComponent,
        SampleByNameComponent,
        AddSampleComponent,
        HomeComponent,
        SampleFilterPipe
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'samples', component: SampleComponent },
            { path: 'samples-by-status', component: SampleByStatusComponent },
            { path: 'samples-by-name', component: SampleByNameComponent },
            { path: 'add-sample', component: AddSampleComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
};
