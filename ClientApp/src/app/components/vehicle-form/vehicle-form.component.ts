import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { Make } from '../../models/make';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';
import { SaveVehicle, Vehicle } from '../../models/vehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: Make[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    modelId: 0,
    makeId: 0,
    isRegistered: false,
    features: [],
    contact: { name: "", email: "", mobile: ""} 

  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService) { 
      this.route.params.subscribe(p => {
        if (p['id'] != NaN)
          this.vehicle.id = +p['id'];
      })
    }

  ngOnInit() {

    Observable.forkJoin([
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ]).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];
    }, err => {
      if (err.status == 404)
        this.router.navigate(['/']);
    });

    if (this.vehicle.id) {
      this.vehicleService.getVehicle(this.vehicle.id)
      .subscribe(v => {
        if(v.id) {
          this.setVehicle(v);
          this.populateModels();
        }
      }, err => {
        if (err.status == 404)
          this.router.navigate(['/']);
      });
    }
  
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.features = v.features.map(f => f.id);
    this.vehicle.contact = v.contact;
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    let selectedMake = this.makes.find(make => make.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    }
    else {
      let index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    if (this.vehicle.id) {
      this.vehicleService.update(this.vehicle).subscribe(v => {
        console.log(v);
      });
    }
    else {
      this.vehicleService.create(this.vehicle).subscribe(x => console.log(x));
    }
  }

}
