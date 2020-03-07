import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Make } from '../models/make';

@Injectable()
export class VehicleService {

  constructor(private http:HttpClient) { }

  getMakes() : Observable<Make[]> {
    return this.http.get<Make[]>('/api/makes');
  }

  getFeatures() : Observable<any[]> {
    return this.http.get<any[]>('/api/features');
  }

}
