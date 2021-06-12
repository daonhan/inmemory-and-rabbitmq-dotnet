import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { VehicleAppComponent } from './vehicle.app.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';

const routes: Routes = [
    {
        path: '',
        component: VehicleAppComponent,
        children: [
            {
                path: '',
                component: VehicleListComponent
            }
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class VehicleRoutingModule {}
