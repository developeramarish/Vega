import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from '../../models/vehicle';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  vehicles: Vehicle[];
  makes: KeyValuePair[];
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
    .subscribe(m => this.makes = m);

    this.vehicleService.getVehicles()
    .subscribe(vl => this.vehicles = vl);
  }

}
