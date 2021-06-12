import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { VehicleService } from './shared/services/vehicle.service';
import { VehicleRoutingModule } from './vehicle-routing.module';
import { VehicleAppComponent } from './vehicle.app.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { NewVehicleComponent } from './new-vehicle/new-vehicle.component';

@NgModule({
    declarations: [
        VehicleAppComponent,
        VehicleListComponent,
        NewVehicleComponent
    ],
    imports: [
        CommonModule, 
        RouterModule, 
        VehicleRoutingModule,
        FormsModule, 
        ReactiveFormsModule,
    ],
    providers: [VehicleService]
})
export class VehicleModule {}
