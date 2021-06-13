import { Component } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

import { masks } from 'src/app/shared/utils/masks';
import { Vehicle } from '../../shared/models/vehicle';
import { VehicleService } from '../../shared/services/vehicle.service';

@Component({
    selector: 'app-remove-vehicle',
    templateUrl: './remove-vehicle.component.html'
})
export class RemoveVehicleComponent {
    public vehicle!: Vehicle;
    public errors: string[] = [];
    public submitting: boolean = false;
    public plateMask: string = masks.plate;

    constructor(
        private vehicleService: VehicleService,
        private bsModalRef: BsModalRef,
        private toastrService: ToastrService
    ) { }

    public confirm(): void {
        this.submitting = true;
        this.vehicleService.remove(this.vehicle.id).subscribe(
            () => {
                this.toastrService.info('Vehicle successfully removed, wait some seconds and it will not be on list.');
                this.close();
            },
            failure => {
                this.errors = failure.error.errors.Messages;
                this.submitting = false;
            }
        )
    }

    public close(): void {
        this.bsModalRef.hide();
    }
}
