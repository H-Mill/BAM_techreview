import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ManageAstronautsComponent } from './manage-astronauts/manage-astronauts.component';
import { ManageAstronautComponent } from './manage-astronaut/manage-astronaut.component';
import { AddDutyComponent } from './add-duty/add-duty.component';

const routes: Routes = [
  { path: '', component: HomeComponent, title: 'ACTS - Astronaut Career Tracking' },
  { path: 'manage-astronauts', component: ManageAstronautsComponent, title: 'ACTS - Manage Astronauts' },
  { path: 'manage-astronaut', component: ManageAstronautComponent, title: 'ACTS - Manage Astronaut'},
  { path: 'manage-astronaut/:personId', component: ManageAstronautComponent, title: 'ACTS - Manage Astronaut' },
  { path: 'add-duty/:personId', component: AddDutyComponent, title: 'ACTS - Add Duty' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
