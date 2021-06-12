import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { Vehicle } from '../models/vehicle';

@Injectable()
export class VehicleService {
    constructor(private http: HttpClient) {}

    public getAll(): Observable<Vehicle[]> {
        return this.http.get<Vehicle[]>(`${environment.apiUrl}/vehicles`);
    }

    public getById(id: string): Observable<Vehicle> {
        return this.http.get<Vehicle>(`${environment.apiUrl}/vehicles/${id}`);
    }

    public new(vehicle: Vehicle): Observable<any> {
        return this.http.post(`${environment.apiUrl}/vehicles`, vehicle);
    }
}