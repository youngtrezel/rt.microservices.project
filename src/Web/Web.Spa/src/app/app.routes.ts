import { Routes } from '@angular/router';
import { SalesComponent } from './sales/sales.component';
import { CommercialComponent } from './commercial/commercial.component';
import { MarketingComponent } from './marketing/marketing.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [

    { path: '', component: HomeComponent },
    { path: 'sales', component: SalesComponent },
    { path: 'commercial', component: CommercialComponent },
    { path: 'marketing', component: MarketingComponent }

];
