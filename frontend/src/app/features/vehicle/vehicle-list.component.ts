import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

import { Subscription, timer } from 'rxjs';
import { map } from 'rxjs/operators';

import { masks } from 'src/app/shared/utils/masks';
import { Vehicle } from './shared/models/vehicle';
import { VehicleService } from './shared/services/vehicle.service';
import { AddVehicleComponent } from './components/add-vehicle/add-vehicle.component';
import { UpdateVehicleComponent } from './components/update-vehicle/update-vehicle.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { RemoveVehicleComponent } from './components/remove-vehicle/remove-vehicle.component';

@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css'],
})
export class VehicleListComponent implements OnInit, OnDestroy {
    public vehicles: Vehicle[] = [];
    public timerSubscription!: Subscription;
    public plateMask: string = masks.plate;

    constructor(
        private vehicleService: VehicleService,
        private modalService: BsModalService
    ) { }

    public ngOnInit(): void {
        this.timerSubscription = timer(0, 5000).pipe(
            map(() => {
                this.loadVehicles();
            })
        ).subscribe();
    }

    public showAddVehicleModal(): void {
        this.modalService.show(AddVehicleComponent);
    }

    public showUpdateVehicleModal(vehicle: Vehicle): void {
        this.modalService.show(UpdateVehicleComponent, {
            initialState: {
                vehicle: vehicle
            }
        });
    }

    public showViewVehicleModal(vehicle: Vehicle): void {
        this.modalService.show(ViewVehicleComponent, {
            initialState: {
                vehicle: vehicle
            }
        });
    }

    public showRemoveVehicleModal(vehicle: Vehicle): void {
        this.modalService.show(RemoveVehicleComponent, {
            initialState: {
                vehicle: vehicle
            }
        });
    }

    public loadVehicles(): void {
        this.vehicleService.getAll().subscribe((vehicles) => {
            this.vehicles = vehicles;
        });
    }

    public ngOnDestroy(): void {
        this.timerSubscription.unsubscribe();
    }
}
