import { Component } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { masks } from 'src/app/shared/utils/masks';
import { Vehicle } from '../../shared/models/vehicle';

@Component({
    selector: 'app-view-vehicle',
    templateUrl: './view-vehicle.component.html'
})
export class ViewVehicleComponent {
    public vehicle!: Vehicle;
    public plateMask: string = masks.plate;

    constructor(
        private bsModalRef: BsModalRef
    ) { }

    public close(): void {
        this.bsModalRef.hide();
    }
}
