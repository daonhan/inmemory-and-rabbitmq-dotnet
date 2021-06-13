import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

import { Vehicle } from '../../shared/models/vehicle';
import { VehicleService } from '../../shared/services/vehicle.service';

@Component({
    selector: 'app-add-vehicle',
    templateUrl: './add-vehicle.component.html'
})
export class AddVehicleComponent implements OnInit {
    public form!: FormGroup;
    public vehicle!: Vehicle;
    public errors: string[] = [];
    public submitting: boolean = false;

    constructor(
        private vehicleService: VehicleService,
        private formBuilder: FormBuilder,
        private bsModalRef: BsModalRef,
        private toastrService: ToastrService
    ) { }

    public ngOnInit(): void {
        this.form = this.formBuilder.group({
            plate: ['', Validators.required]
        })
    }

    public get plate() {
        return this.form.get('plate');
    }

    public confirm(): void {
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        this.submitting = true;
        this.vehicle = Object.assign({}, this.vehicle, this.form.value);
        this.vehicleService.add(this.vehicle).subscribe(
            () => {
                this.toastrService.info('Vehicle successfully added, wait some seconds and it will be on list.');
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
