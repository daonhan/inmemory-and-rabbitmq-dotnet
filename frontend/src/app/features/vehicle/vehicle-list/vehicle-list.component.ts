import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

import { Subscription, timer } from 'rxjs';
import { map } from 'rxjs/operators';

import { NewVehicleComponent } from '../new-vehicle/new-vehicle.component';
import { Vehicle } from '../shared/models/vehicle';
import { VehicleService } from '../shared/services/vehicle.service';

@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css'],
})
export class VehicleListComponent implements OnInit, OnDestroy {
    public vehicles: Vehicle[] = [];
    public timerSubscription!: Subscription;

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

    public showNewVehicleModal(): void {
        this.modalService.show(NewVehicleComponent);
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
