import { Component, OnInit } from '@angular/core';

import { MakeService } from '../../services/make.service';
import { Make } from '../../models/make';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: Make[];
  models: any[];
  vehicle: any = {};

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(makes => {
      this.makes = makes;
    })
  }

  onMakeChange() {
    let selectedMake = this.makes.find(make => make.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

}
