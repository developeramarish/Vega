import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Make } from '../models/make';
import { Vehicle, SaveVehicle } from '../models/vehicle';

@Injectable()
export class VehicleService {
  private readonly vehicleEndPoint = '/api/vehicles';

  constructor(private http:HttpClient) { }

  getMakes() : Observable<Make[]> {
    return this.http.get<Make[]>('/api/makes');
  }

  getFeatures() : Observable<any[]> {
    return this.http.get<any[]>('/api/features');
  }

  getVehicle(id: number): Observable<Vehicle> {
    return this.http.get<Vehicle>(this.vehicleEndPoint + "/" + id);
  }

  getVehicles(filter): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.vehicleEndPoint + '?' + this.toQueryString(filter));
  }

  toQueryString(obj) {
    const parts = [];

    for (let property in obj) {
      let value = obj[property];
      
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }

  create(vehicle): Observable<any> {
    return this.http.post<any>(this.vehicleEndPoint, vehicle);
  }

  update(vehicle: SaveVehicle): Observable<Vehicle> {
    return this.http.put<Vehicle>(this.vehicleEndPoint + vehicle.id, vehicle);
  }

  delete(id: number): Observable<number> {
    return this.http.delete<number>(this.vehicleEndPoint + id);
  }

}
