import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Make } from '../models/make';
import { Vehicle, SaveVehicle } from '../models/vehicle';

@Injectable()
export class VehicleService {

  constructor(private http:HttpClient) { }

  getMakes() : Observable<Make[]> {
    return this.http.get<Make[]>('/api/makes');
  }

  getFeatures() : Observable<any[]> {
    return this.http.get<any[]>('/api/features');
  }

  getVehicle(id: number): Observable<Vehicle> {
    return this.http.get<Vehicle>('/api/vehicles/' + id);
  }

  create(vehicle): Observable<any> {
    return this.http.post<any>('/api/vehicles', vehicle);
  }

  update(vehicle: SaveVehicle): Observable<Vehicle> {
    return this.http.put<Vehicle>('/api/vehicles/' + vehicle.id, vehicle);
  }

  delete(id: number): Observable<number> {
    return this.http.delete<number>('/api/vehicles/' + id);
  }

}
