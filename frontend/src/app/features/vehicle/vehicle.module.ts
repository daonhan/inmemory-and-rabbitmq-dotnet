import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AddVehicleComponent } from './components/add-vehicle/add-vehicle.component';
import { UpdateVehicleComponent } from './components/update-vehicle/update-vehicle.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { RemoveVehicleComponent } from './components/remove-vehicle/remove-vehicle.component';
import { VehicleService } from './shared/services/vehicle.service';
import { VehicleListComponent } from './vehicle-list.component';
import { VehicleRoutingModule } from './vehicle-routing.module';
import { NgxMaskModule } from 'ngx-mask';

@NgModule({
    declarations: [
        VehicleListComponent,
        AddVehicleComponent,
        UpdateVehicleComponent,
        ViewVehicleComponent,
        RemoveVehicleComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        VehicleRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        NgxMaskModule.forChild()
    ],
    providers: [VehicleService]
})
export class VehicleModule { }
