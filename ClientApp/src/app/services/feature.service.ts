import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class FeatureService {

  constructor(private http:HttpClient) { }

  getFeatures() : Observable<any[]> {
    return this.http.get<any[]>('/api/features');
  }

}
