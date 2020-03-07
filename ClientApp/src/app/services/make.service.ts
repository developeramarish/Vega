import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Make } from '../models/make';

@Injectable()
export class MakeService {

  constructor(private http:HttpClient) { }

  getMakes() : Observable<Make[]> {
    return this.http.get<Make[]>('/api/makes');
  }

}
